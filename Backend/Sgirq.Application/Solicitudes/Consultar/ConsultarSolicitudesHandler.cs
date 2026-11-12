using MediatR;
using Sgirq.Application.Salidas.Listar;
using Sgirq.Domain.Repositories;
namespace Sgirq.Application.Solicitudes.Consultar;
// El modelo actual permite consultar salidas por solicitante; no almacena aprobaciones.
public sealed class ConsultarSolicitudesHandler(ISalidaRepository salidas, IReactivoRepository reactivos,
    IPersonalRepository personal) : IRequestHandler<ConsultarSolicitudesQuery, List<SalidaDto>>
{
    public async Task<List<SalidaDto>> Handle(ConsultarSolicitudesQuery request, CancellationToken ct)
    {
        var registros = await salidas.ListarAsync(null, null, request.IdUsuario, null, ct);
        var nombres = (await reactivos.ListarAsync(null, null, null, ct))
            .ToDictionary(r => r.IdReactivo, r => r.NombreComercial);
        var responsables = (await personal.ListarAsync(null, ct)).ToDictionary(
            p => p.IdPersonal, p => $"{p.NombrePersonal} {p.ApellidosPersonal}");
        return registros.Select(s => new SalidaDto(s.IdSalida, s.FechaSalida, s.MotivoSalida,
            s.IdUsuario, s.IdPersonal, responsables.GetValueOrDefault(s.IdPersonal, ""),
            s.Requiere.Select(i => new ItemSalidaDto(i.IdReactivo,
                nombres.GetValueOrDefault(i.IdReactivo, ""), i.CantidadSolicitada)).ToList())).ToList();
    }
}
