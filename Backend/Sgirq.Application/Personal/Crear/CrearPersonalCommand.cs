using MediatR;
namespace Sgirq.Application.Personal.Crear;
public record CrearPersonalCommand(string NombrePersonal, string ApellidosPersonal,
    string UsuarioPersonalLogin, string Contrasenia, string TipoPersonal) : IRequest<int>;
