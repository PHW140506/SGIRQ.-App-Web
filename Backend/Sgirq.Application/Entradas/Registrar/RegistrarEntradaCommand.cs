using MediatR;
namespace Sgirq.Application.Entradas.Registrar;
public record RegistrarEntradaCommand(int IdReactivo, decimal Cantidad) : IRequest<decimal>;
