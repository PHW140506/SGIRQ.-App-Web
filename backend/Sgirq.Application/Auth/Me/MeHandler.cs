using MediatR;
using Microsoft.EntityFrameworkCore;
using Sgirq.Application.Abstractions;
namespace Sgirq.Application.Auth.Me;
public sealed class MeHandler(ISgirqDb db) : IRequestHandler<MeQuery, MeResult?>
{
    public Task<MeResult?> Handle(MeQuery request, CancellationToken ct) => db.Personal.AsNoTracking()
        .Where(p => p.IdPersonal == request.IdPersonal)
        .Select(p => new MeResult(p.IdPersonal, p.NombrePersonal + " " + p.ApellidosPersonal,
            p.UsuarioPersonalLogin, p.TipoPersonal)).FirstOrDefaultAsync(ct);
}
