using MediatR;
using Sgirq.Application.Abstractions;
using Sgirq.Application.Common;
namespace Sgirq.Application.Entradas.Registrar;
public sealed class RegistrarEntradaHandler(ISgirqDb db) : IRequestHandler<RegistrarEntradaCommand, decimal>
{
    public async Task<decimal> Handle(RegistrarEntradaCommand x, CancellationToken ct)
    {
        await using var transaction = await db.BeginTransactionAsync(ct);
        var reactivo = await db.BloquearReactivoAsync(x.IdReactivo, ct)
            ?? throw new BusinessException("Reactivo no registrado", 404);
        if (reactivo.CantidadReactivo + x.Cantidad > 99999999.99m)
            throw new BusinessException("Cantidad fuera del rango permitido");
        reactivo.CantidadReactivo += x.Cantidad;
        await db.SaveChangesAsync(ct);
        await transaction.CommitAsync(ct);
        return reactivo.CantidadReactivo;
    }
}
