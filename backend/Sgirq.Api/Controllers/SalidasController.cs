using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sgirq.Application.Salidas.Listar;
using Sgirq.Application.Salidas.Registrar;
namespace Sgirq.Api.Controllers;
[ApiController]
[Authorize(Roles = "Administrador,Auxiliar")]
[Route("api/salidas")]
public sealed class SalidasController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Registrar(RegistrarSalidaCommand command, CancellationToken ct)
        => Ok(new { idSalida = await sender.Send(command, ct) });
    [Authorize(Roles = "Administrador")]
    [HttpGet]
    public async Task<IActionResult> Historial([FromQuery] DateTime? desde,
        [FromQuery] DateTime? hasta, CancellationToken ct)
        => Ok(await sender.Send(new ListarSalidasQuery(desde, hasta), ct));
}
