using MediatR;
namespace Sgirq.Application.Auth.Login;
public record LoginCommand(string Usuario, string Contrasenia) : IRequest<LoginResult?>;
public record LoginResult(string Token, int IdPersonal, string Nombre, string Rol);
