using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sgirq.Api.Extensions;
using Sgirq.Application.Reportes.Generar;
using Sgirq.Application.Reportes.Listar;
namespace Sgirq.Api.Controllers;
[ApiController]
[Authorize(Roles = "Administrador")]
[Route("api/reportes")]
public sealed class ReportesController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Listar(CancellationToken ct) =>
        Ok(await sender.Send(new ListarReportesQuery(), ct));
    [HttpGet("pdf")]
    public async Task<IActionResult> Generar([FromQuery] string tipo, [FromQuery] DateTime? desde,
        [FromQuery] DateTime? hasta, CancellationToken ct)
    {
        var archivo = await sender.Send(new GenerarReporteCommand(tipo, desde, hasta, User.PersonalId()), ct);
        return File(archivo, "application/pdf", $"sgirq-{tipo}.pdf");
    }
}
