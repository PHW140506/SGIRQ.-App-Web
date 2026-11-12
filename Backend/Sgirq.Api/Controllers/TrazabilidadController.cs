using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sgirq.Application.Trazabilidad.Consultar;
namespace Sgirq.Api.Controllers;
[ApiController]
[Authorize(Roles = "Administrador")]
[Route("api/trazabilidad")]
public sealed class TrazabilidadController(ISender sender) : ControllerBase
{
    [HttpGet("reactivo/{id:int}")]
    public async Task<IActionResult> Consultar(int id, CancellationToken ct) =>
        Ok(await sender.Send(new ConsultarTrazabilidadQuery(id), ct));
}
