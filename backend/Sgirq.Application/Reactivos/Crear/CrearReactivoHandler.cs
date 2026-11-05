using MediatR;
using Microsoft.EntityFrameworkCore;
using Sgirq.Application.Abstractions;
using Sgirq.Application.Common;
using Sgirq.Domain.Entities;
namespace Sgirq.Application.Reactivos.Crear;
public sealed class CrearReactivoHandler(ISgirqDb db) : IRequestHandler<CrearReactivoCommand, int>
{
    public async Task<int> Handle(CrearReactivoCommand x, CancellationToken ct)
    {
        if (await db.Reactivos.AnyAsync(r => r.NumeroCAS == x.NumeroCAS, ct))
            throw new BusinessException("El número CAS ya está registrado", 409);
        if (!await db.Personal.AnyAsync(p => p.IdPersonal == x.IdPersonal, ct))
            throw new BusinessException("El responsable no existe");
        var r = new Reactivo {
            NombreComercial = x.NombreComercial.Trim(), NombreQuimico = x.NombreQuimico.Trim(),
            NumeroCAS = x.NumeroCAS.Trim(), EstadoFisico = x.EstadoFisico,
            CantidadReactivo = x.CantidadReactivo, UnidadMedida = x.UnidadMedida,
            StockMinimo = x.StockMinimo, ColorAnaquel = x.ColorAnaquel, IdPersonal = x.IdPersonal
        };
        db.Reactivos.Add(r);
        await db.SaveChangesAsync(ct);
        return r.IdReactivo;
    }
}
