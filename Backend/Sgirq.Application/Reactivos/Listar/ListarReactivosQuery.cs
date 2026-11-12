using MediatR;
namespace Sgirq.Application.Reactivos.Listar;
public record ListarReactivosQuery(string? Buscar, string? EstadoFisico, string? ColorAnaquel)
    : IRequest<List<ReactivoDto>>;
