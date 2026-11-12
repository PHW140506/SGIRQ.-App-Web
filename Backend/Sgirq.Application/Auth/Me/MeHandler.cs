using MediatR;
using Sgirq.Domain.Repositories;
namespace Sgirq.Application.Auth.Me;
public sealed class MeHandler(IPersonalRepository personal) : IRequestHandler<MeQuery, MeResult?>
{
    public async Task<MeResult?> Handle(MeQuery request, CancellationToken ct)
    {
        var cuenta = await personal.PorIdAsync(request.IdPersonal, ct);
        return cuenta is null ? null : new(cuenta.IdPersonal,
            cuenta.NombrePersonal + " " + cuenta.ApellidosPersonal,
            cuenta.UsuarioPersonalLogin, cuenta.TipoPersonal);
    }
}
