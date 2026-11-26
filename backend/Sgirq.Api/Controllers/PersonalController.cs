using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sgirq.Application.Personal.Crear;
using Sgirq.Application.Personal.Editar;
using Sgirq.Application.Personal.Listar;

namespace Sgirq.Api.Controllers;

[ApiController]
[Authorize(Roles = "Administrador")]
[Route("api/personal")]
public sealed class PersonalController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Listar([FromQuery] string? buscar, CancellationToken ct)
    {
        var query = new ListarPersonalQuery(buscar);
        return Ok(await mediator.Send(query, ct));
    }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CrearPersonalCommand command, CancellationToken ct)
    {
        return Ok(await mediator.Send(command, ct));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Editar([FromRoute] int id, [FromBody] EditarPersonalDatos request, CancellationToken ct)
    {
        var command = new EditarPersonalCommand(id, request);
        await mediator.Send(command, ct);
        return NoContent();
    }
}
