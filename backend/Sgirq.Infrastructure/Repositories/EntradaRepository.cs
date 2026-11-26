using Microsoft.EntityFrameworkCore;
using Sgirq.Domain.Entities;
using Sgirq.Domain.Repositories;
using Sgirq.Infrastructure.Persistence;
namespace Sgirq.Infrastructure.Repositories;
public sealed class EntradaRepository(SgirqDbContext db) : IEntradaRepository
{
    public async Task AgregarAsync(Entrada entrada, CancellationToken ct) => await db.Entradas.AddAsync(entrada, ct);
    public Task<List<Entrada>> ListarAsync(DateTime? desde, DateTime? hasta, CancellationToken ct)
    {
        var query = db.Entradas.AsNoTracking().AsQueryable();
        if (desde.HasValue) query = query.Where(x => x.FechaEntrada >= desde.Value);
        if (hasta.HasValue) query = query.Where(x => x.FechaEntrada < hasta.Value.Date.AddDays(1));
        return query.OrderBy(x => x.FechaEntrada).ToListAsync(ct);
    }
}
