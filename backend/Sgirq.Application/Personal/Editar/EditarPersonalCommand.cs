using MediatR;
namespace Sgirq.Application.Personal.Editar;
public sealed record EditarPersonalCommand(int IdPersonal, EditarPersonalDatos Datos) : IRequest<Unit>;
