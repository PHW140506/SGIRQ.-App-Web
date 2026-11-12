using MediatR;
using Sgirq.Application.Abstractions;
using Sgirq.Domain.Repositories;
namespace Sgirq.Application.Auth.Login;
public sealed class LoginHandler(IPersonalRepository personal, IPasswords passwords, ITokens tokens)
    : IRequestHandler<LoginCommand, LoginResult?>
{
    public async Task<LoginResult?> Handle(LoginCommand request, CancellationToken ct)
    {
        var cuenta = await personal.PorUsuarioAsync(request.Usuario, ct);
        if (cuenta is null || !passwords.Verify(request.Contrasenia, cuenta.Contrasenia)) return null;
        return new(tokens.Generate(cuenta), cuenta.IdPersonal,
            cuenta.NombrePersonal + " " + cuenta.ApellidosPersonal, cuenta.TipoPersonal);
    }
}
