using MediatR;
using Sgirq.Domain.Repositories;
namespace Sgirq.Application.Trazabilidad.Consultar;
public sealed class ConsultarTrazabilidadHandler(ISalidaRepository salidas, IPersonalRepository personal)
    : IRequestHandler<ConsultarTrazabilidadQuery, List<TrazabilidadItem>>
{
    public async Task<List<TrazabilidadItem>> Handle(ConsultarTrazabilidadQuery request, CancellationToken ct)
    {
        var registros = await salidas.ListarAsync(null, null, null, request.IdReactivo, ct);
        var responsables = (await personal.ListarAsync(null, ct)).ToDictionary(
            p => p.IdPersonal, p => $"{p.NombrePersonal} {p.ApellidosPersonal}");
        return registros.Select(s => new TrazabilidadItem(s.IdSalida, s.FechaSalida,
            s.IdUsuario, responsables.GetValueOrDefault(s.IdPersonal, ""), s.MotivoSalida,
            s.Requiere.First(i => i.IdReactivo == request.IdReactivo).CantidadSolicitada)).ToList();
    }
}
