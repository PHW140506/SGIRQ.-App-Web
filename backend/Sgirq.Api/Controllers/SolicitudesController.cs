using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sgirq.Application.Solicitudes.Consultar;

namespace Sgirq.Api.Controllers;

[ApiController]
[Authorize(Roles = "Administrador,Auxiliar")]
[Route("api/solicitudes")]
public sealed class SolicitudesController(IMediator mediator) : ControllerBase
{
    [HttpGet("usuario/{id:int}")]
    public async Task<IActionResult> PorSolicitante([FromRoute] int id, CancellationToken ct)
    {
        var query = new ConsultarSolicitudesQuery(id);
        return Ok(await mediator.Send(query, ct));
    }
}
