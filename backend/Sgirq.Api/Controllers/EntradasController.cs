using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sgirq.Application.Entradas.Registrar;

namespace Sgirq.Api.Controllers;

[ApiController]
[Authorize(Roles = "Administrador")]
[Route("api/entradas")]
public sealed class EntradasController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Registrar([FromBody] RegistrarEntradaCommand command, CancellationToken ct)
    {
        return Ok(await mediator.Send(command, ct));
    }
}
