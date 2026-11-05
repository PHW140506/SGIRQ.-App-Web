using MediatR;
using Microsoft.EntityFrameworkCore;
using Sgirq.Application.Abstractions;
using Sgirq.Application.Common;
namespace Sgirq.Application.Personal.Editar;
public sealed class EditarPersonalHandler(ISgirqDb db) : IRequestHandler<EditarPersonalCommand, Unit>
{
    public async Task<Unit> Handle(EditarPersonalCommand x, CancellationToken ct)
    {
        var p = await db.Personal.FirstOrDefaultAsync(p => p.IdPersonal == x.IdPersonal, ct)
            ?? throw new BusinessException("Personal inexistente", 404);
        p.NombrePersonal = x.NombrePersonal.Trim();
        p.ApellidosPersonal = x.ApellidosPersonal.Trim();
        p.TipoPersonal = x.TipoPersonal;
        await db.SaveChangesAsync(ct);
        return Unit.Value;
    }
}
