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
public sealed class PersonalController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Listar([FromQuery] string? buscar, CancellationToken ct)
        => Ok(await sender.Send(new ListarPersonalQuery(buscar), ct));
    [HttpPost]
    public async Task<IActionResult> Crear(CrearPersonalCommand command, CancellationToken ct)
        => Ok(new { idPersonal = await sender.Send(command, ct) });
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Editar(int id, EditarPersonalRequest request, CancellationToken ct)
    {
        await sender.Send(new EditarPersonalCommand(id, request.NombrePersonal,
            request.ApellidosPersonal, request.TipoPersonal), ct);
        return NoContent();
    }
}
public record EditarPersonalRequest(string NombrePersonal, string ApellidosPersonal, string TipoPersonal);
