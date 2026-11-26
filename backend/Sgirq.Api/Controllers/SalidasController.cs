using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sgirq.Application.Salidas.Listar;
using Sgirq.Application.Salidas.Registrar;

namespace Sgirq.Api.Controllers;

[ApiController]
[Authorize(Roles = "Administrador,Auxiliar")]
[Route("api/salidas")]
public sealed class SalidasController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Registrar([FromBody] RegistrarSalidaCommand command, CancellationToken ct)
    {
        return Ok(await mediator.Send(command, ct));
    }

    [Authorize(Roles = "Administrador")]
    [HttpGet]
    public async Task<IActionResult> Historial([FromQuery] DateTime? desde,
        [FromQuery] DateTime? hasta, CancellationToken ct)
    {
        var query = new ListarSalidasQuery(desde, hasta);
        return Ok(await mediator.Send(query, ct));
    }
}
