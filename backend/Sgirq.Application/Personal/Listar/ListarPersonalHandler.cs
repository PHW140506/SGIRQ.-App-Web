using MediatR;
using Sgirq.Application.Abstractions;
using Sgirq.Domain.Repositories;
namespace Sgirq.Application.Personal.Listar;
public sealed class ListarPersonalHandler(IPersonalRepository personal, IMapper mapper) : IRequestHandler<ListarPersonalQuery, List<PersonalDto>>
{
    public async Task<List<PersonalDto>> Handle(ListarPersonalQuery request, CancellationToken ct)
    {
        var cuentas = await personal.ListarAsync(request.Buscar, ct);
        return cuentas.Select(mapper.PersonalDto).ToList();
    }
}
