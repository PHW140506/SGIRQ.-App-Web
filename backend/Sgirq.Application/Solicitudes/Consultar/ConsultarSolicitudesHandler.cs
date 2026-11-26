using Sgirq.Application.Abstractions;
using MediatR;
using Sgirq.Application.Salidas.Listar;
using Sgirq.Domain.Repositories;
namespace Sgirq.Application.Solicitudes.Consultar;
public sealed class ConsultarSolicitudesHandler(ISalidaRepository salidas, ISalidaProyeccion proyeccion) : IRequestHandler<ConsultarSolicitudesQuery, List<SalidaDto>>
{
    public async Task<List<SalidaDto>> Handle(ConsultarSolicitudesQuery request, CancellationToken ct) =>
        await proyeccion.ConvertirAsync(await salidas.ListarAsync(null, null, request.IdUsuario, null, ct), ct);
}
