using FluentValidation;
namespace Sgirq.Application.Reactivos.Detalle;
public sealed class DetalleReactivoValidator : AbstractValidator<DetalleReactivoQuery>
{
    public DetalleReactivoValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}
