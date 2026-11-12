using MediatR;
using Sgirq.Application.Abstractions;
using Sgirq.Application.Common;
using Sgirq.Domain.Repositories;
namespace Sgirq.Application.Personal.Crear;
public sealed class CrearPersonalHandler(IPersonalRepository personal, IUnidadDeTrabajo unidad, IPasswords passwords)
    : IRequestHandler<CrearPersonalCommand, int>
{
    public async Task<int> Handle(CrearPersonalCommand x, CancellationToken ct)
    {
        if (await personal.UsuarioOcupadoAsync(x.UsuarioPersonalLogin.Trim(), ct))
            throw new BusinessException("El usuario de acceso ya existe", 409);
        var nuevo = new Sgirq.Domain.Entities.Personal {
            NombrePersonal = x.NombrePersonal.Trim(), ApellidosPersonal = x.ApellidosPersonal.Trim(),
            UsuarioPersonalLogin = x.UsuarioPersonalLogin.Trim(), Contrasenia = passwords.Hash(x.Contrasenia),
            TipoPersonal = x.TipoPersonal
        };
        await personal.AgregarAsync(nuevo, ct);
        await unidad.GuardarAsync(ct);
        return nuevo.IdPersonal;
    }
}
