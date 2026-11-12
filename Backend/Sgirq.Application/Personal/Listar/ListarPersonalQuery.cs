using MediatR;
namespace Sgirq.Application.Personal.Listar;
public record ListarPersonalQuery(string? Buscar) : IRequest<List<PersonalDto>>;
