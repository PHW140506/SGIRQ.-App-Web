using MediatR;
using Sgirq.Domain.Repositories;
namespace Sgirq.Application.Alertas.Consultar;
public sealed class ConsultarAlertasHandler(IReactivoRepository reactivos)
    : IRequestHandler<ConsultarAlertasQuery, AlertasResultado>
{
    public async Task<AlertasResultado> Handle(ConsultarAlertasQuery request, CancellationToken ct)
    {
        var lista = await reactivos.ListarAsync(null, null, null, ct);
        var alertas = lista.Where(r => r.CantidadReactivo <= r.StockMinimo)
            .Select(r => new AlertaReactivo(r.IdReactivo, r.NombreComercial,
                r.CantidadReactivo, r.StockMinimo, r.UnidadMedida,
                r.CantidadReactivo == 0 ? "Agotado" : "Bajo stock")).ToList();
        return new(lista.Count, alertas.Count(a => a.Estado == "Bajo stock"),
            alertas.Count(a => a.Estado == "Agotado"), alertas);
    }
}
