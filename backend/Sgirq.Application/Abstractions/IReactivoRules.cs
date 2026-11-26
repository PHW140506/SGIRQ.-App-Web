namespace Sgirq.Application.Abstractions;
public interface IReactivoRules
{
    Task ComprobarAsync(IReactivoIdentidad datos, int? excepto, CancellationToken ct);
}
