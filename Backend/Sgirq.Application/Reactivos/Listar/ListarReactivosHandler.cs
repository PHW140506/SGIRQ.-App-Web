using MediatR;
using Sgirq.Domain.Repositories;
namespace Sgirq.Application.Reactivos.Listar;
public sealed class ListarReactivosHandler(IReactivoRepository reactivos)
    : IRequestHandler<ListarReactivosQuery, List<ReactivoDto>>
{
    public async Task<List<ReactivoDto>> Handle(ListarReactivosQuery x, CancellationToken ct)
    {
        var lista = await reactivos.ListarAsync(x.Buscar, x.EstadoFisico, x.ColorAnaquel, ct);
        return lista.Select(r => new ReactivoDto(r.IdReactivo, r.NombreComercial,
            r.NombreQuimico, r.NumeroCAS, r.EstadoFisico, r.CantidadReactivo,
            r.UnidadMedida, r.StockMinimo, r.ColorAnaquel, r.IdPersonal)).ToList();
    }
}
