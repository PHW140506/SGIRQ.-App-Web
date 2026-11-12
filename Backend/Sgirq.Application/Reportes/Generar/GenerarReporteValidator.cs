using FluentValidation;
namespace Sgirq.Application.Reportes.Generar;
public sealed class GenerarReporteValidator : AbstractValidator<GenerarReporteCommand>
{
    public GenerarReporteValidator()
    {
        RuleFor(x => x.Tipo).Must(t => new[] { "inventario", "salidas", "consumo", "bajo-stock" }.Contains(t))
            .WithMessage("Tipo de reporte inválido");
        RuleFor(x => x.IdPersonal).GreaterThan(0);
        RuleFor(x => x).Must(x => !x.Desde.HasValue || !x.Hasta.HasValue || x.Desde <= x.Hasta)
            .WithMessage("Rango de fechas incorrecto");
        RuleFor(x => x).Must(x => x.Tipo != "salidas" && x.Tipo != "consumo" ||
                x.Desde.HasValue && x.Hasta.HasValue)
            .WithMessage("El reporte por periodo necesita ambas fechas");
    }
}
