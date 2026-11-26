using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sgirq.Api.Contracts;
using Sgirq.Api.Extensions;
using Sgirq.Application.Reportes.Generar;
using Sgirq.Application.Reportes.Listar;

namespace Sgirq.Api.Controllers;

[ApiController]
[Authorize(Roles = "Administrador")]
[Route("api/reportes")]
public sealed class ReportesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Listar(CancellationToken ct)
    {
        var query = new ListarReportesQuery();
        return Ok(await mediator.Send(query, ct));
    }

    [HttpPost("pdf")]
    public async Task<IActionResult> Generar([FromQuery] ReporteFiltro request, CancellationToken ct)
    {
        var command = new GenerarReporteCommand(request.Tipo, request.Desde, request.Hasta, User.PersonalId());
        var archivo = await mediator.Send(command, ct);
        return File(archivo, "application/pdf", $"sgirq-{request.Tipo}.pdf");
    }
}
