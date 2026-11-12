using Sgirq.Domain.Entities;
namespace Sgirq.Domain.Repositories;
public interface IPersonalRepository
{
    Task<Personal?> PorIdAsync(int id, CancellationToken ct);
    Task<Personal?> PorUsuarioAsync(string usuario, CancellationToken ct);
    Task<bool> ExisteAsync(int id, CancellationToken ct);
    Task<bool> UsuarioOcupadoAsync(string usuario, CancellationToken ct);
    Task<List<Personal>> ListarAsync(string? buscar, CancellationToken ct);
    Task AgregarAsync(Personal personal, CancellationToken ct);
}
