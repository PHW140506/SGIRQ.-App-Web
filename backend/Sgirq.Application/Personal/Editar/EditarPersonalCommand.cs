using MediatR;
namespace Sgirq.Application.Personal.Editar;
public record EditarPersonalCommand(int IdPersonal, string NombrePersonal,
    string ApellidosPersonal, string TipoPersonal) : IRequest<Unit>;
