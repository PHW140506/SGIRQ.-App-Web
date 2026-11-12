using MediatR;
using Sgirq.Application.Abstractions;
using Sgirq.Application.Common;
using Sgirq.Domain.Repositories;
namespace Sgirq.Application.Auth.ChangePassword;
public sealed class ChangePasswordHandler(IPersonalRepository personal, IUnidadDeTrabajo unidad, IPasswords passwords)
    : IRequestHandler<ChangePasswordCommand, Unit>
{
    public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken ct)
    {
        var cuenta = await personal.PorIdAsync(request.IdPersonal, ct)
            ?? throw new BusinessException("Cuenta inexistente", 404);
        if (!passwords.Verify(request.Actual, cuenta.Contrasenia))
            throw new BusinessException("Contraseña actual incorrecta");
        cuenta.Contrasenia = passwords.Hash(request.Nueva);
        await unidad.GuardarAsync(ct);
        return Unit.Value;
    }
}
