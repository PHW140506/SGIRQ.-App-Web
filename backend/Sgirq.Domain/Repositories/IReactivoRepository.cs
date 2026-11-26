using Sgirq.Domain.Entities;
namespace Sgirq.Domain.Repositories;
public interface IReactivoRepository
{
    Task<Reactivo?> PorIdAsync(int id, CancellationToken ct);
    Task<Reactivo?> BloquearAsync(int id, CancellationToken ct);
    Task<bool> NombreComercialOcupadoAsync(string nombre, int? excepto, CancellationToken ct);
    Task<bool> NombreQuimicoOcupadoAsync(string nombre, int? excepto, CancellationToken ct);
    Task<bool> CasOcupadoAsync(string cas, int? excepto, CancellationToken ct);
    Task<List<Reactivo>> ListarAsync(string? buscar, string? estado, string? color, CancellationToken ct);
    Task AgregarAsync(Reactivo reactivo, CancellationToken ct);
}
