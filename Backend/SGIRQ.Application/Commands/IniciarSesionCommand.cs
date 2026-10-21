using FluentValidation;
using MediatR;
using SGIRQ.Application.DTOs;

namespace SGIRQ.Application.Commands;

public record IniciarSesionCommand(
    string Identificador,
    string Password
) : IRequest<AuthResponseDto>;

public class IniciarSesionCommandValidator : AbstractValidator<IniciarSesionCommand>
{
    public IniciarSesionCommandValidator()
    {
        // HU-001 Escenario 3: Campos vacíos solicitan completarse obligatoriamente
        RuleFor(x => x.Identificador)
            .NotEmpty().WithMessage("El usuario o correo institucional es obligatorio.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("La contraseña es obligatoria.");
    }
}