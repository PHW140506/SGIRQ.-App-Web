using MediatR;
using Microsoft.AspNetCore.Mvc;
using SGIRQ.Application.Commands;
using SGIRQ.Domain.ReglasDeNegocio;

namespace SGIRQ.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] IniciarSesionCommand command)
    {
        // HU-001 Escenario 3: Validación básica previa
        if (string.IsNullOrWhiteSpace(command.Identificador) || string.IsNullOrWhiteSpace(command.Password))
        {
            return BadRequest(new { mensaje = "Por favor completa todos los campos requeridos." });
        }

        try
        {
            var respuesta = await _mediator.Send(command);
            return Ok(respuesta); // 200 OK con token y rol
        }
        catch (DomainException ex)
        {
            // HU-001 Escenario 2 y HU-007 Escenario 2: 401 Unauthorized sin revelar detalles internos
            return Unauthorized(new { mensaje = ex.Message, codigo = ex.CodigoHistoriaUsuario });
        }
    }

    // HU-004: Recuperar acceso a cuenta (Escenarios 1 y 3)
    [HttpPost("recuperar-acceso")]
    public async Task<IActionResult> RecuperarAcceso([FromBody] SolicitarRecuperacionCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.Correo))
        {
            return BadRequest(new { mensaje = "El correo electrónico es obligatorio." });
        }

        // MediatR ejecuta SolicitarRecuperacionCommandHandler y retorna la respuesta neutral
        var respuesta = await _mediator.Send(command);
        return Ok(new { mensaje = respuesta });
    }
}