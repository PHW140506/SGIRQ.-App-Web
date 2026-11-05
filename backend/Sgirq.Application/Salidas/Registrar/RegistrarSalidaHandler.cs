using MediatR;
using Microsoft.EntityFrameworkCore;
using Sgirq.Application.Abstractions;
using Sgirq.Application.Common;
using Sgirq.Domain.Entities;
namespace Sgirq.Application.Salidas.Registrar;
public sealed class RegistrarSalidaHandler(ISgirqDb db) : IRequestHandler<RegistrarSalidaCommand, int>
{
    public async Task<int> Handle(RegistrarSalidaCommand x, CancellationToken ct)
    {
        await using var transaction = await db.BeginTransactionAsync(ct);
        var anterior = await db.Salidas.Include(s => s.Requiere)
            .FirstOrDefaultAsync(s => s.IdSalida == x.IdSalida, ct);
        if (anterior is not null)
        {
            var mismaOperacion = anterior.IdUsuario == x.IdUsuario && anterior.IdPersonal == x.IdPersonal
                && anterior.MotivoSalida == x.MotivoSalida.Trim()
                && anterior.Requiere.Count == x.Reactivos.Count
                && x.Reactivos.All(i => anterior.Requiere.Any(r => r.IdReactivo == i.IdReactivo
                    && r.CantidadSolicitada == i.CantidadSolicitada));
            if (!mismaOperacion)
                throw new BusinessException("Identificador de salida reutilizado con información diferente", 409);
            return x.IdSalida;
        }
        if (!await db.Usuarios.AnyAsync(u => u.IdUsuario == x.IdUsuario, ct))
            throw new BusinessException("Solicitante inexistente");
        if (!await db.Personal.AnyAsync(p => p.IdPersonal == x.IdPersonal, ct))
            throw new BusinessException("Personal responsable inexistente");

        // Bloqueo en orden fijo para evitar descuentos simultáneos y deadlocks.
        foreach (var item in x.Reactivos.OrderBy(i => i.IdReactivo))
        {
            var reactivo = await db.BloquearReactivoAsync(item.IdReactivo, ct)
                ?? throw new BusinessException($"Reactivo {item.IdReactivo} inexistente", 404);
            if (reactivo.CantidadReactivo < item.CantidadSolicitada)
                throw new BusinessException($"Sin existencias suficientes: {reactivo.NombreComercial}", 409);
            reactivo.CantidadReactivo -= item.CantidadSolicitada;
        }
        db.Salidas.Add(new Salida {
            IdSalida = x.IdSalida, FechaSalida = x.FechaSalida,
            MotivoSalida = x.MotivoSalida.Trim(), IdUsuario = x.IdUsuario,
            IdPersonal = x.IdPersonal,
            Requiere = x.Reactivos.Select(i => new Requiere {
                IdSalida = x.IdSalida, IdReactivo = i.IdReactivo,
                CantidadSolicitada = i.CantidadSolicitada
            }).ToList()
        });
        await db.SaveChangesAsync(ct);
        await transaction.CommitAsync(ct);
        return x.IdSalida;
    }
}
