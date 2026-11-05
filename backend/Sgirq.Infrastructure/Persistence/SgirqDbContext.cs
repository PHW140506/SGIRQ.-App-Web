using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Sgirq.Application.Abstractions;
using Sgirq.Domain.Entities;
namespace Sgirq.Infrastructure.Persistence;
public sealed class SgirqDbContext(DbContextOptions<SgirqDbContext> options) : DbContext(options), ISgirqDb
{
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Personal> Personal => Set<Personal>();
    public DbSet<Reactivo> Reactivos => Set<Reactivo>();
    public DbSet<Salida> Salidas => Set<Salida>();
    public DbSet<Requiere> Requiere => Set<Requiere>();
    public DbSet<Reporte> Reportes => Set<Reporte>();

    protected override void OnModelCreating(ModelBuilder model)
    {
        model.ApplyConfigurationsFromAssembly(typeof(SgirqDbContext).Assembly);
    }
    public async Task<IDbContextTransactionWrapper> BeginTransactionAsync(CancellationToken ct)
        => new TransactionWrapper(await Database.BeginTransactionAsync(ct));

    public Task<Reactivo?> BloquearReactivoAsync(int id, CancellationToken ct)
        => BloquearAsync(id, ct);

    private async Task<Reactivo?> BloquearAsync(int id, CancellationToken ct)
    {
        // Materializar sin componer sobre FOR UPDATE (no válido como subconsulta).
        var bloqueados = await Reactivos.FromSqlInterpolated(
            $"SELECT * FROM Reactivos WHERE idReactivo = {id} FOR UPDATE").ToListAsync(ct);
        return bloqueados.SingleOrDefault();
    }

    private sealed class TransactionWrapper(IDbContextTransaction inner) : IDbContextTransactionWrapper
    {
        public Task CommitAsync(CancellationToken ct) => inner.CommitAsync(ct);
        public ValueTask DisposeAsync() => inner.DisposeAsync();
    }
}
