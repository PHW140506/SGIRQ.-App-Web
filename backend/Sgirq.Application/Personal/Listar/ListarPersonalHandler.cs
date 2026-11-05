using MediatR;
using Microsoft.EntityFrameworkCore;
using Sgirq.Application.Abstractions;
namespace Sgirq.Application.Personal.Listar;
public sealed class ListarPersonalHandler(ISgirqDb db) : IRequestHandler<ListarPersonalQuery, List<PersonalDto>>
{
    public Task<List<PersonalDto>> Handle(ListarPersonalQuery request, CancellationToken ct)
    {
        var q = db.Personal.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(request.Buscar))
            q = q.Where(x => x.NombrePersonal.Contains(request.Buscar) ||
                x.ApellidosPersonal.Contains(request.Buscar) || x.UsuarioPersonalLogin.Contains(request.Buscar));
        return q.OrderBy(x => x.IdPersonal)
            .Select(x => new PersonalDto(x.IdPersonal, x.NombrePersonal, x.ApellidosPersonal,
                x.UsuarioPersonalLogin, x.TipoPersonal)).ToListAsync(ct);
    }
}
