using Sgirq.Application.Personal.Dtos;
using MediatR;
using Sgirq.Application.Abstractions;
using Sgirq.Application.Common;
using Sgirq.Domain.Repositories;
namespace Sgirq.Application.Personal.Crear;
public sealed class CrearPersonalHandler(IPersonalRepository personal, IUnidadDeTrabajo unidad, IPasswords passwords, IMapper mapper) : IRequestHandler<CrearPersonalCommand, CrearPersonalResponse>
{
    public async Task<CrearPersonalResponse> Handle(CrearPersonalCommand request, CancellationToken ct)
    {
        if (await personal.UsuarioOcupadoAsync(request.UsuarioPersonalLogin.Trim(), ct))
            throw new BusinessException("El usuario de acceso ya existe", FailureKind.Conflict);
        var nuevo = mapper.CrearPersonal(request, passwords.Hash(request.Contrasenia));
        await personal.AgregarAsync(nuevo, ct);
        await unidad.GuardarAsync(ct);
        return new CrearPersonalResponse(nuevo.IdPersonal);
    }
}
