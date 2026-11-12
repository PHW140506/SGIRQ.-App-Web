using MediatR;
using Sgirq.Application.Common;
using Sgirq.Domain.Repositories;
namespace Sgirq.Application.Salidas.Listar;
public sealed class ListarSalidasHandler(ISalidaRepository salidas, IReactivoRepository reactivos,
    IPersonalRepository personal) : IRequestHandler<ListarSalidasQuery, List<SalidaDto>>
{
    public async Task<List<SalidaDto>> Handle(ListarSalidasQuery x, CancellationToken ct)
    {
        if (x.Desde.HasValue && x.Hasta.HasValue && x.Desde > x.Hasta)
            throw new BusinessException("El rango de fechas es incorrecto");
        var lista = await salidas.ListarAsync(x.Desde, x.Hasta, null, null, ct);
        var inventario = await reactivos.ListarAsync(null, null, null, ct);
        var empleados = await personal.ListarAsync(null, ct);
        var nombres = inventario.ToDictionary(r => r.IdReactivo, r => r.NombreComercial);
        var responsables = empleados.ToDictionary(p => p.IdPersonal,
            p => p.NombrePersonal + " " + p.ApellidosPersonal);
        return lista.Select(s => new SalidaDto(s.IdSalida, s.FechaSalida, s.MotivoSalida,
            s.IdUsuario, s.IdPersonal, responsables.GetValueOrDefault(s.IdPersonal, ""),
            s.Requiere.Select(i => new ItemSalidaDto(i.IdReactivo,
                nombres.GetValueOrDefault(i.IdReactivo, ""), i.CantidadSolicitada)).ToList())).ToList();
    }
}
