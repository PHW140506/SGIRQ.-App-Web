using MediatR;
using Sgirq.Domain.Repositories;
namespace Sgirq.Application.Personal.Listar;
public sealed class ListarPersonalHandler(IPersonalRepository personal)
    : IRequestHandler<ListarPersonalQuery, List<PersonalDto>>
{
    public async Task<List<PersonalDto>> Handle(ListarPersonalQuery request, CancellationToken ct)
    {
        var cuentas = await personal.ListarAsync(request.Buscar, ct);
        return cuentas.Select(p => new PersonalDto(p.IdPersonal, p.NombrePersonal,
            p.ApellidosPersonal, p.UsuarioPersonalLogin, p.TipoPersonal)).ToList();
    }
}
