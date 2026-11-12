using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sgirq.Application.Alertas.Consultar;
namespace Sgirq.Api.Controllers;
[ApiController]
[Authorize(Roles = "Administrador,Auxiliar")]
[Route("api/alertas")]
public sealed class AlertasController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Consultar(CancellationToken ct) =>
        Ok(await sender.Send(new ConsultarAlertasQuery(), ct));
}
