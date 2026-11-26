using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sgirq.Application.Trazabilidad.Consultar;

namespace Sgirq.Api.Controllers;

[ApiController]
[Authorize(Roles = "Administrador")]
[Route("api/trazabilidad")]
public sealed class TrazabilidadController(IMediator mediator) : ControllerBase
{
    [HttpGet("reactivo/{id:int}")]
    public async Task<IActionResult> Consultar([FromRoute] int id, CancellationToken ct)
    {
        var query = new ConsultarTrazabilidadQuery(id);
        return Ok(await mediator.Send(query, ct));
    }
}
