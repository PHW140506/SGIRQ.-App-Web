using Microsoft.EntityFrameworkCore;
using Sgirq.Domain.Entities;
using Sgirq.Domain.Repositories;
using Sgirq.Infrastructure.Persistence;
namespace Sgirq.Infrastructure.Repositories;
public sealed class PersonalRepository(SgirqDbContext db) : IPersonalRepository
{
    public Task<Personal?> PorIdAsync(int id, CancellationToken ct) =>
        db.Personal.FirstOrDefaultAsync(p => p.IdPersonal == id, ct);
    public Task<Personal?> PorUsuarioAsync(string usuario, CancellationToken ct) =>
        db.Personal.AsNoTracking().FirstOrDefaultAsync(p => p.UsuarioPersonalLogin == usuario, ct);
    public Task<bool> ExisteAsync(int id, CancellationToken ct) =>
        db.Personal.AnyAsync(p => p.IdPersonal == id, ct);
    public Task<bool> UsuarioOcupadoAsync(string usuario, CancellationToken ct) =>
        db.Personal.AnyAsync(p => p.UsuarioPersonalLogin == usuario, ct);
    public Task<List<Personal>> ListarAsync(string? buscar, CancellationToken ct)
    {
        var consulta = db.Personal.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(buscar))
            consulta = consulta.Where(p => p.NombrePersonal.Contains(buscar) ||
                p.ApellidosPersonal.Contains(buscar) || p.UsuarioPersonalLogin.Contains(buscar));
        return consulta.OrderBy(p => p.IdPersonal).ToListAsync(ct);
    }
    public async Task AgregarAsync(Personal personal, CancellationToken ct)
    {
        await db.Personal.AddAsync(personal, ct);
    }
}
