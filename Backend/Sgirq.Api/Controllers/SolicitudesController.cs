using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sgirq.Application.Solicitudes.Consultar;
namespace Sgirq.Api.Controllers;
[ApiController]
[Authorize(Roles = "Administrador,Auxiliar")]
[Route("api/solicitudes")]
public sealed class SolicitudesController(ISender sender) : ControllerBase
{
    [HttpGet("usuario/{id:int}")]
    public async Task<IActionResult> PorSolicitante(int id, CancellationToken ct) =>
        Ok(await sender.Send(new ConsultarSolicitudesQuery(id), ct));
}
