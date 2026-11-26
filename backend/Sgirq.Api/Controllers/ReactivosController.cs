using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sgirq.Api.Contracts;
using Sgirq.Application.Reactivos.Crear;
using Sgirq.Application.Reactivos.Detalle;
using Sgirq.Application.Reactivos.Editar;
using Sgirq.Application.Reactivos.Listar;

namespace Sgirq.Api.Controllers;

[ApiController]
[Authorize(Roles = "Administrador,Auxiliar")]
[Route("api/reactivos")]
public sealed class ReactivosController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Listar([FromQuery] ReactivosFiltro request, CancellationToken ct)
    {
        var query = new ListarReactivosQuery(request.Buscar, request.EstadoFisico, request.ColorAnaquel);
        return Ok(await mediator.Send(query, ct));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Detalle([FromRoute] int id, CancellationToken ct)
    {
        var query = new DetalleReactivoQuery(id);
        return Ok(await mediator.Send(query, ct));
    }

    [Authorize(Roles = "Administrador")]
    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CrearReactivoCommand command, CancellationToken ct)
    {
        return Ok(await mediator.Send(command, ct));
    }

    [Authorize(Roles = "Administrador")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Editar([FromRoute] int id, [FromBody] EditarReactivoDatos request, CancellationToken ct)
    {
        var command = new EditarReactivoCommand(id, request);
        await mediator.Send(command, ct);
        return NoContent();
    }
}
