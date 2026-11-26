using Sgirq.Application.Abstractions;
using Sgirq.Domain.Entities;
using Sgirq.Domain.Repositories;
namespace Sgirq.Infrastructure.Seed;
public sealed class InicializadorDatos(IPersonalRepository personal, IUsuarioRepository usuarios,
    IUnidadDeTrabajo unidad, IPasswords passwords) : IInicializadorDatos
{
    public async Task CrearDatosInicialesAsync(string? contrasenia, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(contrasenia)) return;
        if (!(await personal.ListarAsync(null, ct)).Any())
            await personal.AgregarAsync(new Personal {
                NombrePersonal = "Administrador", ApellidosPersonal = "SGIRQ",
                UsuarioPersonalLogin = "admin", Contrasenia = passwords.Hash(contrasenia),
                TipoPersonal = "Administrador"
            }, ct);
        if (!(await usuarios.ListarAsync(ct)).Any())
            await usuarios.AgregarAsync(new Usuario(), ct);
        await unidad.GuardarAsync(ct);
    }
}
