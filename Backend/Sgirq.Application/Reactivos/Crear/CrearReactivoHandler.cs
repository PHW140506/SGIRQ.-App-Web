using MediatR;
using Sgirq.Application.Common;
using Sgirq.Domain.Entities;
using Sgirq.Domain.Repositories;
namespace Sgirq.Application.Reactivos.Crear;
public sealed class CrearReactivoHandler(IReactivoRepository reactivos, IPersonalRepository personal,
    IUnidadDeTrabajo unidad) : IRequestHandler<CrearReactivoCommand, int>
{
    public async Task<int> Handle(CrearReactivoCommand x, CancellationToken ct)
    {
        if (await reactivos.CasOcupadoAsync(x.NumeroCAS.Trim(), null, ct))
            throw new BusinessException("El número CAS ya está registrado", 409);
        if (!await personal.ExisteAsync(x.IdPersonal, ct))
            throw new BusinessException("El responsable no existe");
        var nuevo = new Reactivo {
            NombreComercial = x.NombreComercial.Trim(), NombreQuimico = x.NombreQuimico.Trim(),
            NumeroCAS = x.NumeroCAS.Trim(), EstadoFisico = x.EstadoFisico,
            CantidadReactivo = x.CantidadReactivo, UnidadMedida = x.UnidadMedida,
            StockMinimo = x.StockMinimo, ColorAnaquel = x.ColorAnaquel, IdPersonal = x.IdPersonal
        };
        await reactivos.AgregarAsync(nuevo, ct);
        await unidad.GuardarAsync(ct);
        return nuevo.IdReactivo;
    }
}
