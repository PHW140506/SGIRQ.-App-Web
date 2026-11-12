using MediatR;
using Sgirq.Domain.Repositories;
namespace Sgirq.Application.Solicitantes.Listar;
public sealed class ListarSolicitantesHandler(IUsuarioRepository usuarios)
    : IRequestHandler<ListarSolicitantesQuery, List<int>>
{
    public async Task<List<int>> Handle(ListarSolicitantesQuery request, CancellationToken ct) =>
        (await usuarios.ListarAsync(ct)).Select(u => u.IdUsuario).ToList();
}
