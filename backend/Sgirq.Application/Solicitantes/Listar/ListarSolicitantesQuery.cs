using MediatR;
namespace Sgirq.Application.Solicitantes.Listar;
public record ListarSolicitantesQuery : IRequest<List<int>>;
