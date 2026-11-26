using MediatR;
using Sgirq.Application.Abstractions;
using Sgirq.Domain.Repositories;
namespace Sgirq.Application.Reactivos.Listar;
public sealed class ListarReactivosHandler(IReactivoRepository reactivos, IMapper mapper) : IRequestHandler<ListarReactivosQuery, List<ReactivoDto>>
{
    public async Task<List<ReactivoDto>> Handle(ListarReactivosQuery request, CancellationToken ct)
    {
        var lista = await reactivos.ListarAsync(request.Buscar, request.EstadoFisico, request.ColorAnaquel, ct);
        return lista.Select(mapper.ReactivoDto).ToList();
    }
}
