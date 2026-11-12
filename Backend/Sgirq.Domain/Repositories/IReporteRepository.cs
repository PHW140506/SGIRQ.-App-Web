using Sgirq.Domain.Entities;
namespace Sgirq.Domain.Repositories;
public interface IReporteRepository
{
    Task AgregarAsync(Reporte reporte, CancellationToken ct);
    Task<List<Reporte>> ListarAsync(CancellationToken ct);
}
