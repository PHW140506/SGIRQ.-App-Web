using MediatR;
using Microsoft.EntityFrameworkCore;
using Sgirq.Application.Abstractions;
namespace Sgirq.Application.Reactivos.Listar;
public sealed class ListarReactivosHandler(ISgirqDb db) : IRequestHandler<ListarReactivosQuery, List<ReactivoDto>>
{
    public Task<List<ReactivoDto>> Handle(ListarReactivosQuery x, CancellationToken ct)
    {
        var q = db.Reactivos.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(x.Buscar))
            q = q.Where(r => r.NombreComercial.Contains(x.Buscar) ||
                r.NombreQuimico.Contains(x.Buscar) || r.NumeroCAS.Contains(x.Buscar));
        if (!string.IsNullOrWhiteSpace(x.EstadoFisico)) q = q.Where(r => r.EstadoFisico == x.EstadoFisico);
        if (!string.IsNullOrWhiteSpace(x.ColorAnaquel)) q = q.Where(r => r.ColorAnaquel == x.ColorAnaquel);
        return q.OrderBy(r => r.NombreComercial).Select(r => new ReactivoDto(r.IdReactivo,
            r.NombreComercial, r.NombreQuimico, r.NumeroCAS, r.EstadoFisico,
            r.CantidadReactivo, r.UnidadMedida, r.StockMinimo, r.ColorAnaquel, r.IdPersonal)).ToListAsync(ct);
    }
}
