using FluentValidation;
namespace Sgirq.Application.Trazabilidad.Consultar;
public sealed class ConsultarTrazabilidadValidator : AbstractValidator<ConsultarTrazabilidadQuery>
{
    public ConsultarTrazabilidadValidator() => RuleFor(x => x.IdReactivo).GreaterThan(0);
}
