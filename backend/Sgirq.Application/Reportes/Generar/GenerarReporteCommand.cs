using MediatR;
namespace Sgirq.Application.Reportes.Generar;
public record GenerarReporteCommand(string Tipo, DateTime? Desde, DateTime? Hasta, int IdPersonal)
    : IRequest<byte[]>;
