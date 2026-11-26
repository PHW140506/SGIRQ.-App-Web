using Sgirq.Application.Entradas.Dtos;
using MediatR;
using Sgirq.Application.Common;
using Sgirq.Domain.Entities;
using Sgirq.Domain.Repositories;
namespace Sgirq.Application.Entradas.Registrar;
public sealed class RegistrarEntradaHandler(IReactivoRepository reactivos, IEntradaRepository entradas, IUnidadDeTrabajo unidad) : IRequestHandler<RegistrarEntradaCommand, RegistrarEntradaResponse>
{
    public async Task<RegistrarEntradaResponse> Handle(RegistrarEntradaCommand request, CancellationToken ct)
    {
        await using var transaccion = await unidad.IniciarTransaccionAsync(ct);
        var reactivo = await reactivos.BloquearAsync(request.IdReactivo, ct)
            ?? throw new BusinessException("Reactivo no registrado", FailureKind.NotFound);
        reactivo.Ingresar(request.Cantidad);
        await entradas.AgregarAsync(new Entrada
        {
            IdReactivo = request.IdReactivo, Cantidad = request.Cantidad,
            FechaEntrada = request.FechaEntrada ?? DateTime.Now
        }, ct);
        var cantidad = reactivo.CantidadReactivo;
        await unidad.GuardarAsync(ct);
        await transaccion.ConfirmarAsync(ct);
        return new RegistrarEntradaResponse(cantidad);
    }
}
