using MediatR;
namespace Sgirq.Application.Reactivos.Detalle;
public record DetalleReactivoQuery(int Id) : IRequest<ReactivoDto?>;
