using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sgirq.Application.Solicitantes.Listar;
namespace Sgirq.Api.Controllers;
[ApiController]
[Authorize(Roles = "Administrador,Auxiliar")]
[Route("api/solicitantes")]
public sealed class SolicitantesController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Listar(CancellationToken ct) =>
        Ok(await sender.Send(new ListarSolicitantesQuery(), ct));
}
