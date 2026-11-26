using Sgirq.Domain.Entities;
namespace Sgirq.Domain.Repositories;
public interface IUsuarioRepository
{
    Task<bool> ExisteAsync(int id, CancellationToken ct);
    Task<List<Usuario>> ListarAsync(CancellationToken ct);
    Task AgregarAsync(Usuario usuario, CancellationToken ct);
}
