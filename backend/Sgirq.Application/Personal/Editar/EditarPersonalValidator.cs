using FluentValidation;
using Sgirq.Domain.Rules;
namespace Sgirq.Application.Personal.Editar;
public sealed class EditarPersonalValidator : AbstractValidator<EditarPersonalCommand>
{
    public EditarPersonalValidator()
    {
        RuleFor(x => x.IdPersonal).GreaterThan(0);
        RuleFor(x => x.Datos).NotNull();
        When(x => x.Datos is not null, () =>
        {
            RuleFor(x => x.Datos.NombrePersonal).NotEmpty().MaximumLength(30);
            RuleFor(x => x.Datos.ApellidosPersonal).NotEmpty().MaximumLength(45);
            RuleFor(x => x.Datos.TipoPersonal).Must(ValoresPermitidos.Roles.Contains);
        });
    }
}
