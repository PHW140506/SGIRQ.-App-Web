using FluentValidation;
namespace Sgirq.Application.Solicitudes.Consultar;
public sealed class ConsultarSolicitudesValidator : AbstractValidator<ConsultarSolicitudesQuery>
{
    public ConsultarSolicitudesValidator() => RuleFor(x => x.IdUsuario).GreaterThan(0);
}
