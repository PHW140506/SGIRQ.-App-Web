using MediatR;
using Microsoft.EntityFrameworkCore;
using Sgirq.Application.Abstractions;
namespace Sgirq.Application.Auth.Login;
public sealed class LoginHandler(ISgirqDb db, IPasswords passwords, ITokens tokens)
    : IRequestHandler<LoginCommand, LoginResult?>
{
    public async Task<LoginResult?> Handle(LoginCommand request, CancellationToken ct)
    {
        var personal = await db.Personal.AsNoTracking()
            .FirstOrDefaultAsync(x => x.UsuarioPersonalLogin == request.Usuario, ct);
        if (personal is null || !passwords.Verify(request.Contrasenia, personal.Contrasenia)) return null;
        return new(tokens.Generate(personal), personal.IdPersonal,
            personal.NombrePersonal + " " + personal.ApellidosPersonal, personal.TipoPersonal);
    }
}
