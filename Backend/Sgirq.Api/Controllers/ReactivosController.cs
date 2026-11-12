using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sgirq.Application.Reactivos.Crear;
using Sgirq.Application.Reactivos.Detalle;
using Sgirq.Application.Reactivos.Editar;
using Sgirq.Application.Reactivos.Listar;
namespace Sgirq.Api.Controllers;
[ApiController]
[Authorize(Roles = "Administrador,Auxiliar")]
[Route("api/reactivos")]
public sealed class ReactivosController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Listar([FromQuery] string? buscar, [FromQuery] string? estadoFisico,
        [FromQuery] string? colorAnaquel, CancellationToken ct)
        => Ok(await sender.Send(new ListarReactivosQuery(buscar, estadoFisico, colorAnaquel), ct));
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Detalle(int id, CancellationToken ct)
    {
        var result = await sender.Send(new DetalleReactivoQuery(id), ct);
        return result is null ? NotFound(new { message = "Reactivo no encontrado" }) : Ok(result);
    }
    [Authorize(Roles = "Administrador")]
    [HttpPost]
    public async Task<IActionResult> Crear(CrearReactivoCommand command, CancellationToken ct)
        => Ok(new { idReactivo = await sender.Send(command, ct) });
    [Authorize(Roles = "Administrador")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Editar(int id, EditarReactivoRequest x, CancellationToken ct)
    {
        await sender.Send(new EditarReactivoCommand(id, x.NombreComercial, x.NombreQuimico,
            x.NumeroCAS, x.EstadoFisico, x.UnidadMedida, x.StockMinimo,
            x.ColorAnaquel, x.IdPersonal), ct);
        return NoContent();
    }
}
public record EditarReactivoRequest(string NombreComercial, string NombreQuimico,
    string NumeroCAS, string EstadoFisico, string UnidadMedida, decimal StockMinimo,
    string ColorAnaquel, int IdPersonal);
