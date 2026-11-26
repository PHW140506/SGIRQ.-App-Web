using Sgirq.Domain.Entities;
using Sgirq.Application.Salidas.Listar;
namespace Sgirq.Application.Abstractions;
public interface ISalidaProyeccion
{
    Task<List<SalidaDto>> ConvertirAsync(IEnumerable<Salida> salidas, CancellationToken ct);
}
