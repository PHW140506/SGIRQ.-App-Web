using Sgirq.Application.Abstractions;
using MediatR;
using Sgirq.Domain.Repositories;
namespace Sgirq.Application.Salidas.Listar;
public sealed class ListarSalidasHandler(ISalidaRepository salidas, ISalidaProyeccion proyeccion) : IRequestHandler<ListarSalidasQuery, List<SalidaDto>>
{
    public async Task<List<SalidaDto>> Handle(ListarSalidasQuery request, CancellationToken ct)
    {
        return await proyeccion.ConvertirAsync(await salidas.ListarAsync(request.Desde, request.Hasta, null, null, ct), ct);
    }
}
