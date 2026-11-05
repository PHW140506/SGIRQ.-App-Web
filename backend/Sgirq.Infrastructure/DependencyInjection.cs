using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sgirq.Application.Abstractions;
using Sgirq.Infrastructure.Persistence;
using Sgirq.Infrastructure.Security;
namespace Sgirq.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var connection = config.GetConnectionString("Default")
            ?? throw new InvalidOperationException("Configura ConnectionStrings:Default en User Secrets");
        services.AddDbContext<SgirqDbContext>(o => o.UseMySql(connection, new MySqlServerVersion(new Version(8, 0, 36))));
        services.AddScoped<ISgirqDb>(sp => sp.GetRequiredService<SgirqDbContext>());
        services.AddScoped<IPasswords, BCryptPasswords>();
        return services;
    }
}
