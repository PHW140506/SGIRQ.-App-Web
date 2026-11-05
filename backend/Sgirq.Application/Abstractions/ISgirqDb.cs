using Microsoft.EntityFrameworkCore;
using Sgirq.Domain.Entities;
namespace Sgirq.Application.Abstractions;
public interface ISgirqDb
{
   DbSet<Sgirq.Domain.Entities.Personal> Personal { get; }
    DbSet<Usuario> Usuarios { get; }
    DbSet<Reactivo> Reactivos { get; }
    DbSet<Salida> Salidas { get; }
    DbSet<Requiere> Requiere { get; }
    Task<int> SaveChangesAsync(CancellationToken ct = default);
    Task<IDbContextTransactionWrapper> BeginTransactionAsync(CancellationToken ct);
    Task<Reactivo?> BloquearReactivoAsync(int id, CancellationToken ct);
}
public interface IDbContextTransactionWrapper : IAsyncDisposable
{
    Task CommitAsync(CancellationToken ct);
}
