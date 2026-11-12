using MediatR;
using Sgirq.Domain.Repositories;
namespace Sgirq.Application.Reactivos.Detalle;
public sealed class DetalleReactivoHandler(IReactivoRepository reactivos)
    : IRequestHandler<DetalleReactivoQuery, ReactivoDto?>
{
    public async Task<ReactivoDto?> Handle(DetalleReactivoQuery x, CancellationToken ct)
    {
        var r = await reactivos.PorIdAsync(x.Id, ct);
        return r is null ? null : new ReactivoDto(r.IdReactivo, r.NombreComercial,
            r.NombreQuimico, r.NumeroCAS, r.EstadoFisico, r.CantidadReactivo,
            r.UnidadMedida, r.StockMinimo, r.ColorAnaquel, r.IdPersonal);
    }
}
