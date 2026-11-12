namespace Sgirq.Application.Abstractions;
public interface IInicializadorDatos
{
    Task CrearDatosInicialesAsync(string? contrasenia, CancellationToken ct);
}
