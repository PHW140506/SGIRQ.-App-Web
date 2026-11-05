using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sgirq.Application.Entradas.Registrar;
namespace Sgirq.Api.Controllers;
[ApiController]
[Authorize(Roles = "Administrador")]
[Route("api/entradas")]
public sealed class EntradasController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Registrar(RegistrarEntradaCommand command, CancellationToken ct)
        => Ok(new { cantidadActual = await sender.Send(command, ct) });
}
