using MediatR;
namespace Sgirq.Application.Trazabilidad.Consultar;
public record ConsultarTrazabilidadQuery(int IdReactivo) : IRequest<List<TrazabilidadItem>>;
public record TrazabilidadItem(int IdSalida, DateTime FechaSalida, int IdUsuario,
    string Responsable, string Motivo, decimal CantidadSolicitada);
