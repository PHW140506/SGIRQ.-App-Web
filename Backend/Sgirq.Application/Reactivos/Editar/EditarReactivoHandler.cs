using MediatR;
using Sgirq.Application.Common;
using Sgirq.Domain.Repositories;
namespace Sgirq.Application.Reactivos.Editar;
public sealed class EditarReactivoHandler(IReactivoRepository reactivos, IPersonalRepository personal,
    IUnidadDeTrabajo unidad) : IRequestHandler<EditarReactivoCommand, Unit>
{
    public async Task<Unit> Handle(EditarReactivoCommand x, CancellationToken ct)
    {
        var r = await reactivos.PorIdAsync(x.IdReactivo, ct)
            ?? throw new BusinessException("Reactivo inexistente", 404);
        if (await reactivos.CasOcupadoAsync(x.NumeroCAS.Trim(), x.IdReactivo, ct))
            throw new BusinessException("El número CAS ya está registrado", 409);
        if (!await personal.ExisteAsync(x.IdPersonal, ct))
            throw new BusinessException("El responsable no existe");
        r.NombreComercial = x.NombreComercial.Trim();
        r.NombreQuimico = x.NombreQuimico.Trim();
        r.NumeroCAS = x.NumeroCAS.Trim();
        r.EstadoFisico = x.EstadoFisico;
        r.UnidadMedida = x.UnidadMedida;
        r.StockMinimo = x.StockMinimo;
        r.ColorAnaquel = x.ColorAnaquel;
        r.IdPersonal = x.IdPersonal;
        await unidad.GuardarAsync(ct);
        return Unit.Value;
    }
}
