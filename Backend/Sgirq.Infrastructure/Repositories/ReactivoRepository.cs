using Microsoft.EntityFrameworkCore;
using Sgirq.Domain.Entities;
using Sgirq.Domain.Repositories;
using Sgirq.Infrastructure.Persistence;
namespace Sgirq.Infrastructure.Repositories;
public sealed class ReactivoRepository(SgirqDbContext db) : IReactivoRepository
{
    public Task<Reactivo?> PorIdAsync(int id, CancellationToken ct) =>
        db.Reactivos.FirstOrDefaultAsync(r => r.IdReactivo == id, ct);
    public async Task<Reactivo?> BloquearAsync(int id, CancellationToken ct)
    {
        // El SELECT FOR UPDATE se realiza dentro de una transacción.
        var filas = await db.Reactivos.FromSqlInterpolated(
            $"SELECT * FROM Reactivos WHERE idReactivo = {id} FOR UPDATE").ToListAsync(ct);
        return filas.SingleOrDefault();
    }
    public Task<bool> CasOcupadoAsync(string cas, int? excepto, CancellationToken ct) =>
        db.Reactivos.AnyAsync(r => r.NumeroCAS == cas && (!excepto.HasValue || r.IdReactivo != excepto.Value), ct);
    public Task<List<Reactivo>> ListarAsync(string? buscar, string? estado, string? color, CancellationToken ct)
    {
        var consulta = db.Reactivos.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(buscar))
            consulta = consulta.Where(r => r.NombreComercial.Contains(buscar) ||
                r.NombreQuimico.Contains(buscar) || r.NumeroCAS.Contains(buscar));
        if (!string.IsNullOrWhiteSpace(estado)) consulta = consulta.Where(r => r.EstadoFisico == estado);
        if (!string.IsNullOrWhiteSpace(color)) consulta = consulta.Where(r => r.ColorAnaquel == color);
        return consulta.OrderBy(r => r.NombreComercial).ToListAsync(ct);
    }
    public async Task AgregarAsync(Reactivo reactivo, CancellationToken ct)
    {
        await db.Reactivos.AddAsync(reactivo, ct);
    }
}
