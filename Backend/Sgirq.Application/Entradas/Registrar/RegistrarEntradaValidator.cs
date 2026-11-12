using FluentValidation;
namespace Sgirq.Application.Entradas.Registrar;
public sealed class RegistrarEntradaValidator : AbstractValidator<RegistrarEntradaCommand>
{
    public RegistrarEntradaValidator()
    {
        RuleFor(x => x.IdReactivo).GreaterThan(0);
        RuleFor(x => x.Cantidad).GreaterThan(0).PrecisionScale(10, 2, true);
    }
}
