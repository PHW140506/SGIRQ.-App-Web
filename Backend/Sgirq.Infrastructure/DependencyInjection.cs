using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sgirq.Application.Abstractions;
using Sgirq.Infrastructure.Persistence;
using Sgirq.Infrastructure.Security;
using Sgirq.Infrastructure.Repositories;
using Sgirq.Domain.Repositories;
using Sgirq.Infrastructure.Reports;
using Sgirq.Infrastructure.Seed;
namespace Sgirq.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var connection = config.GetConnectionString("Default")
            ?? throw new InvalidOperationException("Configura ConnectionStrings:Default en User Secrets");
        services.AddDbContext<SgirqDbContext>(o => o.UseMySql(connection, new MySqlServerVersion(new Version(8, 0, 36))));
        services.AddScoped<IPersonalRepository, PersonalRepository>();
        services.AddScoped<IReactivoRepository, ReactivoRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<ISalidaRepository, SalidaRepository>();
        services.AddScoped<IReporteRepository, ReporteRepository>();
        services.AddScoped<IUnidadDeTrabajo, UnidadDeTrabajo>();
        services.AddScoped<IPasswords, BCryptPasswords>();
        services.AddSingleton<IGeneradorPdf, GeneradorPdf>();
        services.AddScoped<IInicializadorDatos, InicializadorDatos>();
        return services;
    }
}
