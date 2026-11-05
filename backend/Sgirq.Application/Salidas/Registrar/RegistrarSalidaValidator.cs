using FluentValidation;
namespace Sgirq.Application.Salidas.Registrar;
public sealed class RegistrarSalidaValidator : AbstractValidator<RegistrarSalidaCommand>
{
    public RegistrarSalidaValidator()
    {
        RuleFor(x => x.IdSalida).GreaterThan(0);
        RuleFor(x => x.FechaSalida).NotEmpty();
        RuleFor(x => x.MotivoSalida).NotEmpty().MaximumLength(255);
        RuleFor(x => x.IdUsuario).GreaterThan(0);
        RuleFor(x => x.IdPersonal).GreaterThan(0);
        RuleFor(x => x.Reactivos).NotEmpty();
        RuleForEach(x => x.Reactivos).ChildRules(item => {
            item.RuleFor(i => i.IdReactivo).GreaterThan(0);
            item.RuleFor(i => i.CantidadSolicitada).GreaterThan(0).PrecisionScale(10, 2, true);
        });
        RuleFor(x => x.Reactivos).Must(items => items is not null &&
            items.Select(i => i.IdReactivo).Distinct().Count() == items.Count)
            .WithMessage("Un mismo reactivo no puede repetirse en la salida");
    }
}
