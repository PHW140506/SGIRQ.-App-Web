using MediatR;
using Microsoft.EntityFrameworkCore;
using Sgirq.Application.Abstractions;
using Sgirq.Application.Common;
namespace Sgirq.Application.Salidas.Listar;
public sealed class ListarSalidasHandler(ISgirqDb db) : IRequestHandler<ListarSalidasQuery, List<SalidaDto>>
{
    public async Task<List<SalidaDto>> Handle(ListarSalidasQuery x, CancellationToken ct)
    {
        if (x.Desde.HasValue && x.Hasta.HasValue && x.Desde > x.Hasta)
            throw new BusinessException("El rango de fechas es incorrecto");
        var q = db.Salidas.AsNoTracking();
        if (x.Desde.HasValue) q = q.Where(s => s.FechaSalida >= x.Desde.Value.Date);
        if (x.Hasta.HasValue) q = q.Where(s => s.FechaSalida < x.Hasta.Value.Date.AddDays(1));
        return await q.OrderByDescending(s => s.FechaSalida).Select(s => new SalidaDto(
            s.IdSalida, s.FechaSalida, s.MotivoSalida, s.IdUsuario, s.IdPersonal,
            db.Personal.Where(p => p.IdPersonal == s.IdPersonal)
                .Select(p => p.NombrePersonal + " " + p.ApellidosPersonal).FirstOrDefault() ?? "",
            db.Requiere.Where(r => r.IdSalida == s.IdSalida).Select(r => new ItemSalidaDto(
                r.IdReactivo, db.Reactivos.Where(a => a.IdReactivo == r.IdReactivo)
                    .Select(a => a.NombreComercial).FirstOrDefault() ?? "", r.CantidadSolicitada)).ToList()
        )).ToListAsync(ct);
    }
}
