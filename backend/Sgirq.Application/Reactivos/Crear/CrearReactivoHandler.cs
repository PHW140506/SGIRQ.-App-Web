using Sgirq.Application.Reactivos.Dtos;
using MediatR;
using Sgirq.Application.Abstractions;
using Sgirq.Domain.Repositories;
namespace Sgirq.Application.Reactivos.Crear;
public sealed class CrearReactivoHandler(IReactivoRepository reactivos, IReactivoRules reglas,
    IMapper mapper, IUnidadDeTrabajo unidad) : IRequestHandler<CrearReactivoCommand, CrearReactivoResponse>
{
    public async Task<CrearReactivoResponse> Handle(CrearReactivoCommand request, CancellationToken ct)
    {
        await reglas.ComprobarAsync(request, null, ct);
        var reactivo = mapper.CrearReactivo(request);
        await reactivos.AgregarAsync(reactivo, ct);
        await unidad.GuardarAsync(ct);
        return new CrearReactivoResponse(reactivo.IdReactivo);
    }
}
