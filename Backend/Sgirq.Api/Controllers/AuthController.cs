using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sgirq.Api.Extensions;
using Sgirq.Application.Auth.ChangePassword;
using Sgirq.Application.Auth.Login;
using Sgirq.Application.Auth.Me;
namespace Sgirq.Api.Controllers;
[ApiController]
[Route("api/auth")]
public sealed class AuthController(ISender sender) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand command, CancellationToken ct)
    {
        var result = await sender.Send(command, ct);
        return result is null ? Unauthorized(new { message = "Usuario o contraseña incorrectos" }) : Ok(result);
    }
    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> Me(CancellationToken ct)
    {
        var result = await sender.Send(new MeQuery(User.PersonalId()), ct);
        return result is null ? Unauthorized() : Ok(result);
    }
    [Authorize]
    [HttpPut("contrasenia")]
    public async Task<IActionResult> Cambiar(ChangePasswordRequest request, CancellationToken ct)
    {
        await sender.Send(new ChangePasswordCommand(User.PersonalId(), request.Actual, request.Nueva), ct);
        return NoContent();
    }
}
public record ChangePasswordRequest(string Actual, string Nueva);
