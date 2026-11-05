using MediatR;
using Microsoft.EntityFrameworkCore;
using Sgirq.Application.Abstractions;
using Sgirq.Application.Common;
namespace Sgirq.Application.Reactivos.Editar;
public sealed class EditarReactivoHandler(ISgirqDb db) : IRequestHandler<EditarReactivoCommand, Unit>
{
    public async Task<Unit> Handle(EditarReactivoCommand x, CancellationToken ct)
    {
        var r = await db.Reactivos.FirstOrDefaultAsync(r => r.IdReactivo == x.IdReactivo, ct)
            ?? throw new BusinessException("Reactivo inexistente", 404);
        if (await db.Reactivos.AnyAsync(a => a.IdReactivo != x.IdReactivo && a.NumeroCAS == x.NumeroCAS, ct))
            throw new BusinessException("El número CAS ya está registrado", 409);
        if (!await db.Personal.AnyAsync(p => p.IdPersonal == x.IdPersonal, ct))
            throw new BusinessException("El responsable no existe");
        r.NombreComercial = x.NombreComercial.Trim(); r.NombreQuimico = x.NombreQuimico.Trim();
        r.NumeroCAS = x.NumeroCAS.Trim(); r.EstadoFisico = x.EstadoFisico;
        r.UnidadMedida = x.UnidadMedida; r.StockMinimo = x.StockMinimo;
        r.ColorAnaquel = x.ColorAnaquel; r.IdPersonal = x.IdPersonal;
        await db.SaveChangesAsync(ct);
        return Unit.Value;
    }
}
