using Sgirq.Domain.Entities;
namespace Sgirq.Domain.Repositories;
public interface IEntradaRepository
{
    Task AgregarAsync(Entrada entrada, CancellationToken ct);
    Task<List<Entrada>> ListarAsync(DateTime? desde, DateTime? hasta, CancellationToken ct);
}
