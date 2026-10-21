using FluentValidation;
using MediatR;

namespace SGIRQ.Application.Commands;

public record SolicitarRecuperacionCommand(string Correo) : IRequest<string>;

public class SolicitarRecuperacionCommandValidator : AbstractValidator<SolicitarRecuperacionCommand>
{
    public SolicitarRecuperacionCommandValidator()
    {
        RuleFor(x => x.Correo)
            .NotEmpty().WithMessage("El correo electrónico es obligatorio.")
            .EmailAddress().WithMessage("El formato del correo electrónico no es válido.");
    }
}