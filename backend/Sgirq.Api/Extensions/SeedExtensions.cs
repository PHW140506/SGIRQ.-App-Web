using Sgirq.Application.Abstractions;
namespace Sgirq.Api.Extensions;
public static class SeedExtensions
{
    public static async Task SeedDevelopmentAsync(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment()) return;
        using var scope = app.Services.CreateScope();
        var inicializador = scope.ServiceProvider.GetRequiredService<IInicializadorDatos>();
        await inicializador.CrearDatosInicialesAsync(app.Configuration["Seed:AdminPassword"],
            CancellationToken.None);
    }
}
