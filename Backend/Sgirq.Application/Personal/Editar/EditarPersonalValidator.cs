using FluentValidation;
using Sgirq.Domain.Rules;
namespace Sgirq.Application.Personal.Editar;
public sealed class EditarPersonalValidator : AbstractValidator<EditarPersonalCommand>
{
    public EditarPersonalValidator()
    {
        RuleFor(x => x.IdPersonal).GreaterThan(0);
        RuleFor(x => x.NombrePersonal).NotEmpty().MaximumLength(30);
        RuleFor(x => x.ApellidosPersonal).NotEmpty().MaximumLength(45);
        RuleFor(x => x.TipoPersonal).Must(ValoresPermitidos.Roles.Contains);
    }
}
