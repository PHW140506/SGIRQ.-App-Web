using MediatR;
using Sgirq.Domain.Repositories;
namespace Sgirq.Application.Reportes.Listar;
public sealed class ListarReportesHandler(IReporteRepository reportes)
    : IRequestHandler<ListarReportesQuery, List<ReporteDto>>
{
    public async Task<List<ReporteDto>> Handle(ListarReportesQuery query, CancellationToken ct) =>
        (await reportes.ListarAsync(ct)).Select(r => new ReporteDto(r.IdReporte,
            r.FechaInicio, r.FechaFinal, r.TipoReporte, r.IdPersonal)).ToList();
}
