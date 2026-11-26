using FluentValidation;
using Sgirq.Domain.Rules;
namespace Sgirq.Application.Reactivos.Editar;
public sealed class EditarReactivoValidator : AbstractValidator<EditarReactivoCommand>
{
    public EditarReactivoValidator()
    {
        RuleFor(x => x.IdReactivo).GreaterThan(0);
        RuleFor(x => x.Datos.NombreComercial).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Datos.NombreQuimico).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Datos.NumeroCAS).NotEmpty().MaximumLength(30);
        RuleFor(x => x.Datos.EstadoFisico).Must(ValoresPermitidos.Estados.Contains);
        RuleFor(x => x.Datos.UnidadMedida).Must(ValoresPermitidos.Unidades.Contains);
        RuleFor(x => x.Datos.ColorAnaquel).Must(ValoresPermitidos.Anaqueles.Contains);
        RuleFor(x => x.Datos.StockMinimo).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Datos.IdPersonal).GreaterThan(0);
    }
}
