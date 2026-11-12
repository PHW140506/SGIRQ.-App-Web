using Microsoft.EntityFrameworkCore;
using Sgirq.Domain.Entities;
using Sgirq.Domain.Repositories;
using Sgirq.Infrastructure.Persistence;
namespace Sgirq.Infrastructure.Repositories;
public sealed class SalidaRepository(SgirqDbContext db) : ISalidaRepository
{
    public Task<Salida?> PorIdAsync(int id, CancellationToken ct) =>
        db.Salidas.Include(s => s.Requiere).AsNoTracking().FirstOrDefaultAsync(s => s.IdSalida == id, ct);
    public Task<List<Salida>> ListarAsync(DateTime? desde, DateTime? hasta, int? usuario, int? reactivo, CancellationToken ct)
    {
        var consulta = db.Salidas.Include(s => s.Requiere).AsNoTracking();
        if (desde.HasValue) consulta = consulta.Where(s => s.FechaSalida >= desde.Value.Date);
        if (hasta.HasValue) consulta = consulta.Where(s => s.FechaSalida < hasta.Value.Date.AddDays(1));
        if (usuario.HasValue) consulta = consulta.Where(s => s.IdUsuario == usuario.Value);
        if (reactivo.HasValue) consulta = consulta.Where(s => s.Requiere.Any(r => r.IdReactivo == reactivo.Value));
        return consulta.OrderByDescending(s => s.FechaSalida).ToListAsync(ct);
    }
    public async Task AgregarAsync(Salida salida, CancellationToken ct)
    {
        await db.Salidas.AddAsync(salida, ct);
    }
}
