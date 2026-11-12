using MediatR;
using Sgirq.Application.Common;
using Sgirq.Domain.Repositories;
namespace Sgirq.Application.Entradas.Registrar;
public sealed class RegistrarEntradaHandler(IReactivoRepository reactivos, IUnidadDeTrabajo unidad)
    : IRequestHandler<RegistrarEntradaCommand, decimal>
{
    public async Task<decimal> Handle(RegistrarEntradaCommand x, CancellationToken ct)
    {
        await using var transaccion = await unidad.IniciarTransaccionAsync(ct);
        var reactivo = await reactivos.BloquearAsync(x.IdReactivo, ct)
            ?? throw new BusinessException("Reactivo no registrado", 404);
        if (reactivo.CantidadReactivo + x.Cantidad > 99999999.99m)
            throw new BusinessException("Cantidad fuera del rango permitido");
        reactivo.CantidadReactivo += x.Cantidad;
        await unidad.GuardarAsync(ct);
        await transaccion.ConfirmarAsync(ct);
        return reactivo.CantidadReactivo;
    }
}
