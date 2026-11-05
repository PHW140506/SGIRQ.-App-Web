using MediatR;
using Microsoft.EntityFrameworkCore;
using Sgirq.Application.Abstractions;
namespace Sgirq.Application.Reactivos.Detalle;
public sealed class DetalleReactivoHandler(ISgirqDb db) : IRequestHandler<DetalleReactivoQuery, ReactivoDto?>
{
    public Task<ReactivoDto?> Handle(DetalleReactivoQuery x, CancellationToken ct) => db.Reactivos
        .AsNoTracking().Where(r => r.IdReactivo == x.Id)
        .Select(r => new ReactivoDto(r.IdReactivo, r.NombreComercial, r.NombreQuimico,
            r.NumeroCAS, r.EstadoFisico, r.CantidadReactivo, r.UnidadMedida,
            r.StockMinimo, r.ColorAnaquel, r.IdPersonal)).FirstOrDefaultAsync(ct);
}
