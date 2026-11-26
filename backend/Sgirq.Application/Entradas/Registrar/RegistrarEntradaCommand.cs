using Sgirq.Application.Entradas.Dtos;
using MediatR;
namespace Sgirq.Application.Entradas.Registrar;
public record RegistrarEntradaCommand(
int IdReactivo, decimal Cantidad, DateTime? FechaEntrada = null)
: IRequest<RegistrarEntradaResponse>;
