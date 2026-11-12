using MediatR;
using Sgirq.Application.Common;
using Sgirq.Domain.Entities;
using Sgirq.Domain.Repositories;
namespace Sgirq.Application.Salidas.Registrar;
public sealed class RegistrarSalidaHandler(ISalidaRepository salidas, IUsuarioRepository usuarios,
    IPersonalRepository personal, IReactivoRepository reactivos, IUnidadDeTrabajo unidad)
    : IRequestHandler<RegistrarSalidaCommand, int>
{
    public async Task<int> Handle(RegistrarSalidaCommand x, CancellationToken ct)
    {
        await using var transaccion = await unidad.IniciarTransaccionAsync(ct);
        var anterior = await salidas.PorIdAsync(x.IdSalida, ct);
        if (anterior is not null)
        {
            if (!EsElMismoEnvio(anterior, x))
                throw new BusinessException("Identificador de salida reutilizado", 409);
            return x.IdSalida;
        }
        if (!await usuarios.ExisteAsync(x.IdUsuario, ct))
            throw new BusinessException("Solicitante inexistente", 404);
        if (!await personal.ExisteAsync(x.IdPersonal, ct))
            throw new BusinessException("Personal responsable inexistente", 404);
        foreach (var item in x.Reactivos.OrderBy(r => r.IdReactivo))
        {
            var reactivo = await reactivos.BloquearAsync(item.IdReactivo, ct)
                ?? throw new BusinessException("Reactivo inexistente", 404);
            if (reactivo.CantidadReactivo < item.CantidadSolicitada)
                throw new BusinessException($"Sin existencias suficientes: {reactivo.NombreComercial}", 409);
            reactivo.CantidadReactivo -= item.CantidadSolicitada;
        }
        var salida = new Salida {
            IdSalida = x.IdSalida, FechaSalida = x.FechaSalida, MotivoSalida = x.MotivoSalida.Trim(),
            IdUsuario = x.IdUsuario, IdPersonal = x.IdPersonal,
            Requiere = x.Reactivos.Select(i => new Requiere {
                IdSalida = x.IdSalida, IdReactivo = i.IdReactivo,
                CantidadSolicitada = i.CantidadSolicitada
            }).ToList()
        };
        await salidas.AgregarAsync(salida, ct);
        await unidad.GuardarAsync(ct);
        await transaccion.ConfirmarAsync(ct);
        return x.IdSalida;
    }
    private static bool EsElMismoEnvio(Salida anterior, RegistrarSalidaCommand x) =>
        anterior.IdUsuario == x.IdUsuario && anterior.IdPersonal == x.IdPersonal &&
        anterior.MotivoSalida == x.MotivoSalida.Trim() && anterior.FechaSalida == x.FechaSalida &&
        anterior.Requiere.Count == x.Reactivos.Count &&
        x.Reactivos.All(i => anterior.Requiere.Any(r => r.IdReactivo == i.IdReactivo &&
            r.CantidadSolicitada == i.CantidadSolicitada));
}
