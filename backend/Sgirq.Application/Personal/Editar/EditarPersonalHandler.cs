using MediatR;
using Sgirq.Application.Abstractions;
using Sgirq.Application.Common;
using Sgirq.Domain.Repositories;
namespace Sgirq.Application.Personal.Editar;
public sealed class EditarPersonalHandler(IPersonalRepository personal, IUnidadDeTrabajo unidad, IMapper mapper) : IRequestHandler<EditarPersonalCommand, Unit>
{
    public async Task<Unit> Handle(EditarPersonalCommand request, CancellationToken ct)
    {
        var cuenta = await personal.PorIdAsync(request.IdPersonal, ct)
            ?? throw new BusinessException("Personal inexistente", FailureKind.NotFound);
        mapper.ActualizarPersonal(request.Datos, cuenta);
        await unidad.GuardarAsync(ct);
        return Unit.Value;
    }
}
