using Microsoft.EntityFrameworkCore.Storage;
using Sgirq.Domain.Repositories;
using Sgirq.Infrastructure.Persistence;
namespace Sgirq.Infrastructure.Repositories;
public sealed class UnidadDeTrabajo(SgirqDbContext db) : IUnidadDeTrabajo
{
    public async Task GuardarAsync(CancellationToken ct) => await db.SaveChangesAsync(ct);
    public async Task<ITransaccion> IniciarTransaccionAsync(CancellationToken ct) =>
        new Transaccion(await db.Database.BeginTransactionAsync(ct));
    private sealed class Transaccion(IDbContextTransaction inner) : ITransaccion
    {
        public Task ConfirmarAsync(CancellationToken ct) => inner.CommitAsync(ct);
        public ValueTask DisposeAsync() => inner.DisposeAsync();
    }
}
