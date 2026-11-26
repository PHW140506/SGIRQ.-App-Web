using Microsoft.EntityFrameworkCore;
using Sgirq.Domain.Entities;
using Sgirq.Domain.Repositories;
using Sgirq.Infrastructure.Persistence;
namespace Sgirq.Infrastructure.Repositories;
public sealed class ReporteRepository(SgirqDbContext db) : IReporteRepository
{
    public async Task AgregarAsync(Reporte reporte, CancellationToken ct)
    {
        await db.Reportes.AddAsync(reporte, ct);
    }
    public Task<List<Reporte>> ListarAsync(CancellationToken ct) =>
        db.Reportes.AsNoTracking().OrderByDescending(r => r.IdReporte).ToListAsync(ct);
}
