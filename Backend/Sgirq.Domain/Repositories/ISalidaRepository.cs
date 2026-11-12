using Sgirq.Domain.Entities;
namespace Sgirq.Domain.Repositories;
public interface ISalidaRepository
{
    Task<Salida?> PorIdAsync(int id, CancellationToken ct);
    Task<List<Salida>> ListarAsync(DateTime? desde, DateTime? hasta, int? usuario, int? reactivo, CancellationToken ct);
    Task AgregarAsync(Salida salida, CancellationToken ct);
}
