using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sgirq.Application.Alertas.Consultar;

namespace Sgirq.Api.Controllers;

[ApiController]
[Authorize(Roles = "Administrador,Auxiliar")]
[Route("api/alertas")]
public sealed class AlertasController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Consultar(CancellationToken ct)
    {
        var query = new ConsultarAlertasQuery();
        return Ok(await mediator.Send(query, ct));
    }
}
