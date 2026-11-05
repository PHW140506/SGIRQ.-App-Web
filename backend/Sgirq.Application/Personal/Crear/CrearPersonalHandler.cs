using MediatR;
using Microsoft.EntityFrameworkCore;
using Sgirq.Application.Abstractions;
using Sgirq.Application.Common;
using Sgirq.Domain.Entities;
namespace Sgirq.Application.Personal.Crear;
public sealed class CrearPersonalHandler(ISgirqDb db, IPasswords passwords)
    : IRequestHandler<CrearPersonalCommand, int>
{
    public async Task<int> Handle(CrearPersonalCommand x, CancellationToken ct)
    {
        if (await db.Personal.AnyAsync(p => p.UsuarioPersonalLogin == x.UsuarioPersonalLogin, ct))
            throw new BusinessException("El usuario de acceso ya existe", 409);
        var personal = new Sgirq.Domain.Entities.Personal {
            NombrePersonal = x.NombrePersonal.Trim(), ApellidosPersonal = x.ApellidosPersonal.Trim(),
            UsuarioPersonalLogin = x.UsuarioPersonalLogin.Trim(), Contrasenia = passwords.Hash(x.Contrasenia),
            TipoPersonal = x.TipoPersonal
        };
        db.Personal.Add(personal);
        await db.SaveChangesAsync(ct);
        return personal.IdPersonal;
    }
}
