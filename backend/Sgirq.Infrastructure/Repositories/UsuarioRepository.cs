using Microsoft.EntityFrameworkCore;
using Sgirq.Domain.Entities;
using Sgirq.Domain.Repositories;
using Sgirq.Infrastructure.Persistence;
namespace Sgirq.Infrastructure.Repositories;
public sealed class UsuarioRepository(SgirqDbContext db) : IUsuarioRepository
{
    public Task<bool> ExisteAsync(int id, CancellationToken ct) =>
        db.Usuarios.AnyAsync(u => u.IdUsuario == id, ct);
    public Task<List<Usuario>> ListarAsync(CancellationToken ct) =>
        db.Usuarios.AsNoTracking().OrderBy(u => u.IdUsuario).ToListAsync(ct);
    public async Task AgregarAsync(Usuario usuario, CancellationToken ct)
    {
        await db.Usuarios.AddAsync(usuario, ct);
    }
}
