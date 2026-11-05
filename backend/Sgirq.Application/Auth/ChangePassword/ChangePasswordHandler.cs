using MediatR;
using Microsoft.EntityFrameworkCore;
using Sgirq.Application.Abstractions;
using Sgirq.Application.Common;
namespace Sgirq.Application.Auth.ChangePassword;
public sealed class ChangePasswordHandler(ISgirqDb db, IPasswords passwords)
    : IRequestHandler<ChangePasswordCommand, Unit>
{
    public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken ct)
    {
        var personal = await db.Personal.FirstOrDefaultAsync(x => x.IdPersonal == request.IdPersonal, ct)
            ?? throw new BusinessException("Cuenta inexistente", 404);
        if (!passwords.Verify(request.Actual, personal.Contrasenia))
            throw new BusinessException("Contraseña actual incorrecta");
        personal.Contrasenia = passwords.Hash(request.Nueva);
        await db.SaveChangesAsync(ct);
        return Unit.Value;
    }
}
