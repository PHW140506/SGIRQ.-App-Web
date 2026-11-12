using MediatR;
namespace Sgirq.Application.Alertas.Consultar;
public record ConsultarAlertasQuery : IRequest<AlertasResultado>;
public record AlertaReactivo(int IdReactivo, string Nombre, decimal Cantidad, decimal StockMinimo,
    string Unidad, string Estado);
public record AlertasResultado(int Total, int BajoStock, int Agotados, List<AlertaReactivo> Alertas);
