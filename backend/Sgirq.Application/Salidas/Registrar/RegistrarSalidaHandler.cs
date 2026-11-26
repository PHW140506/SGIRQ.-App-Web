using Sgirq.Application.Salidas.Dtos;
using MediatR;
using Sgirq.Domain.Entities;
using Sgirq.Application.Common;
using Sgirq.Domain.Repositories;
namespace Sgirq.Application.Salidas.Registrar;
public sealed class RegistrarSalidaHandler(ISalidaRepository salidas, IUsuarioRepository usuarios, IPersonalRepository personal,
    IReactivoRepository reactivos, IUnidadDeTrabajo unidad) : IRequestHandler<RegistrarSalidaCommand, RegistrarSalidaResponse>
{
    public async Task<RegistrarSalidaResponse> Handle(RegistrarSalidaCommand request, CancellationToken ct)
    {
        await using var tx = await unidad.IniciarTransaccionAsync(ct);
        var salida = CrearSalida(request);
        var anterior = await salidas.PorIdAsync(request.IdSalida, ct);
        if (anterior is not null)
        {
            if (!anterior.MismoContenido(salida))
                throw new BusinessException("Identificador de salida reutilizado", FailureKind.Conflict);
            return new RegistrarSalidaResponse(request.IdSalida);
        }
        await ValidarYDescontarAsync(request, ct);
        await salidas.AgregarAsync(salida, ct);
        await unidad.GuardarAsync(ct);
        await tx.ConfirmarAsync(ct);
        return new RegistrarSalidaResponse(request.IdSalida);
    }

    private static Salida CrearSalida(RegistrarSalidaCommand x) => new()
    {
        IdSalida = x.IdSalida,
        FechaSalida = DateTime.SpecifyKind(x.FechaSalida, DateTimeKind.Unspecified),
        MotivoSalida = x.MotivoSalida.Trim(), IdUsuario = x.IdUsuario, IdPersonal = x.IdPersonal,
        Requiere = x.Reactivos.Select(i => new Requiere
        {
            IdSalida = x.IdSalida, IdReactivo = i.IdReactivo,
            CantidadSolicitada = i.CantidadSolicitada
        }).ToList()
    };

    private async Task ValidarYDescontarAsync(RegistrarSalidaCommand x, CancellationToken ct)
    {
        if (!await usuarios.ExisteAsync(x.IdUsuario, ct))
            throw new BusinessException("Usuario solicitante inexistente", FailureKind.NotFound);
        if (!await personal.ExisteAsync(x.IdPersonal, ct))
            throw new BusinessException("Personal responsable inexistente", FailureKind.NotFound);
        foreach (var item in x.Reactivos.OrderBy(i => i.IdReactivo))
        {
            var reactivo = await reactivos.BloquearAsync(item.IdReactivo, ct)
                ?? throw new BusinessException("Reactivo inexistente", FailureKind.NotFound);
            reactivo.Retirar(item.CantidadSolicitada);
        }
    }
}
