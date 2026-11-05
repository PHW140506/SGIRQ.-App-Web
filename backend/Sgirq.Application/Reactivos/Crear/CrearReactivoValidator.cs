using FluentValidation;
using Sgirq.Domain.Rules;
namespace Sgirq.Application.Reactivos.Crear;
public sealed class CrearReactivoValidator : AbstractValidator<CrearReactivoCommand>
{
    public CrearReactivoValidator()
    {
        RuleFor(x => x.NombreComercial).NotEmpty().MaximumLength(50);
        RuleFor(x => x.NombreQuimico).NotEmpty().MaximumLength(50);
        RuleFor(x => x.NumeroCAS).NotEmpty().MaximumLength(30);
        RuleFor(x => x.EstadoFisico).Must(ValoresPermitidos.Estados.Contains);
        RuleFor(x => x.UnidadMedida).Must(ValoresPermitidos.Unidades.Contains);
        RuleFor(x => x.ColorAnaquel).Must(ValoresPermitidos.Anaqueles.Contains);
        RuleFor(x => x.CantidadReactivo).GreaterThanOrEqualTo(0);
        RuleFor(x => x.StockMinimo).GreaterThanOrEqualTo(0);
        RuleFor(x => x.IdPersonal).GreaterThan(0);
    }
}
