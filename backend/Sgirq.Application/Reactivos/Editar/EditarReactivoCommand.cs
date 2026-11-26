using MediatR;
namespace Sgirq.Application.Reactivos.Editar;
public sealed record EditarReactivoCommand(int IdReactivo, EditarReactivoDatos Datos) : IRequest<Unit>;
