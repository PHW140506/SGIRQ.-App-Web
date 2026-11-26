using FluentValidation;

namespace Sgirq.Application.Salidas.Listar;

public sealed class ListarSalidasValidator : AbstractValidator<ListarSalidasQuery>
{
    public ListarSalidasValidator()
    {
        RuleFor(query => query)
            .Must(query => !query.Desde.HasValue || !query.Hasta.HasValue || query.Desde <= query.Hasta)
            .WithMessage("El rango de fechas es incorrecto");
    }
}
