using MediatR;
using Sgirq.Application.Common;
using Sgirq.Domain.Repositories;
namespace Sgirq.Application.Personal.Editar;
public sealed class EditarPersonalHandler(IPersonalRepository personal, IUnidadDeTrabajo unidad)
    : IRequestHandler<EditarPersonalCommand, Unit>
{
    public async Task<Unit> Handle(EditarPersonalCommand x, CancellationToken ct)
    {
        var cuenta = await personal.PorIdAsync(x.IdPersonal, ct)
            ?? throw new BusinessException("Personal inexistente", 404);
        cuenta.NombrePersonal = x.NombrePersonal.Trim();
        cuenta.ApellidosPersonal = x.ApellidosPersonal.Trim();
        cuenta.TipoPersonal = x.TipoPersonal;
        await unidad.GuardarAsync(ct);
        return Unit.Value;
    }
}
