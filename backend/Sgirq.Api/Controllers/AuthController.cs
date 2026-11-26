using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sgirq.Api.Contracts;
using Sgirq.Api.Extensions;
using Sgirq.Application.Auth.ChangePassword;
using Sgirq.Application.Auth.Login;
using Sgirq.Application.Auth.Me;

namespace Sgirq.Api.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController(IMediator mediator) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command, CancellationToken ct)
    {
        return Ok(await mediator.Send(command, ct));
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> Me(CancellationToken ct)
    {
        var query = new MeQuery(User.PersonalId());
        return Ok(await mediator.Send(query, ct));
    }

    [Authorize]
    [HttpPut("contrasenia")]
    public async Task<IActionResult> Cambiar([FromBody] ChangePasswordRequest request, CancellationToken ct)
    {
        var command = new ChangePasswordCommand(User.PersonalId(), request.Actual, request.Nueva);
        await mediator.Send(command, ct);
        return NoContent();
    }
}
