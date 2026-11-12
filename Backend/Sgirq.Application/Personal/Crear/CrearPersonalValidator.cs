using FluentValidation;
using Sgirq.Domain.Rules;
namespace Sgirq.Application.Personal.Crear;
public sealed class CrearPersonalValidator : AbstractValidator<CrearPersonalCommand>
{
    public CrearPersonalValidator()
    {
        RuleFor(x => x.NombrePersonal).NotEmpty().MaximumLength(30);
        RuleFor(x => x.ApellidosPersonal).NotEmpty().MaximumLength(45);
        RuleFor(x => x.UsuarioPersonalLogin).NotEmpty().MaximumLength(25);
        RuleFor(x => x.Contrasenia).NotEmpty().MinimumLength(10);
        RuleFor(x => x.TipoPersonal).Must(ValoresPermitidos.Roles.Contains).WithMessage("Tipo de personal no permitido");
    }
}
