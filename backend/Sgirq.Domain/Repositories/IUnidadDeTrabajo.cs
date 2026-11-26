namespace Sgirq.Domain.Repositories;
public interface IUnidadDeTrabajo
{
    Task GuardarAsync(CancellationToken ct);
    Task<ITransaccion> IniciarTransaccionAsync(CancellationToken ct);
}
public interface ITransaccion : IAsyncDisposable
{
    Task ConfirmarAsync(CancellationToken ct);
}
