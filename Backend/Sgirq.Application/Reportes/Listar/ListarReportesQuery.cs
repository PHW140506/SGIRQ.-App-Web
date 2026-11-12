using MediatR;
namespace Sgirq.Application.Reportes.Listar;
public record ListarReportesQuery : IRequest<List<ReporteDto>>;
public record ReporteDto(int IdReporte, DateTime FechaInicio, DateTime FechaFinal,
    string TipoReporte, int IdPersonal);
