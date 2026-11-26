using MediatR;
using Sgirq.Application.Abstractions;
using Sgirq.Application.Common;
using Sgirq.Domain.Repositories;
namespace Sgirq.Application.Reactivos.Editar;
public sealed class EditarReactivoHandler(IReactivoRepository reactivos, IReactivoRules reglas,
    IMapper mapper, IUnidadDeTrabajo unidad) : IRequestHandler<EditarReactivoCommand, Unit>
{
    public async Task<Unit> Handle(EditarReactivoCommand request, CancellationToken ct)
    {
        var reactivo = await reactivos.PorIdAsync(request.IdReactivo, ct)
            ?? throw new BusinessException("Reactivo inexistente", FailureKind.NotFound);
        await reglas.ComprobarAsync(request.Datos, request.IdReactivo, ct);
        mapper.ActualizarReactivo(request.Datos, reactivo);
        await unidad.GuardarAsync(ct);
        return Unit.Value;
    }
}
