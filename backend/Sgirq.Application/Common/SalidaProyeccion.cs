using Sgirq.Application.Abstractions;
using Sgirq.Domain.Entities;
using Sgirq.Domain.Repositories;
using Sgirq.Application.Salidas.Listar;
namespace Sgirq.Application.Common;
public sealed class SalidaProyeccion(IReactivoRepository reactivos,
    IPersonalRepository personal) : ISalidaProyeccion
{
    public async Task<List<SalidaDto>> ConvertirAsync(IEnumerable<Salida> salidas, CancellationToken ct)
    {
        var nombres = (await reactivos.ListarAsync(null, null, null, ct))
            .ToDictionary(r => r.IdReactivo, r => r.NombreComercial);
        var responsables = (await personal.ListarAsync(null, ct))
            .ToDictionary(p => p.IdPersonal, p => $"{p.NombrePersonal} {p.ApellidosPersonal}");
        return salidas.Select(s => new SalidaDto(s.IdSalida, s.FechaSalida, s.MotivoSalida,
            s.IdUsuario, s.IdPersonal, responsables.GetValueOrDefault(s.IdPersonal, ""),
            s.Requiere.Select(i => new ItemSalidaDto(i.IdReactivo,
                nombres.GetValueOrDefault(i.IdReactivo, ""), i.CantidadSolicitada)).ToList())).ToList();
    }
}
