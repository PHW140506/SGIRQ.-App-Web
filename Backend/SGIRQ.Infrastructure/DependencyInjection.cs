using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SGIRQ.Application.Interfaces;
using SGIRQ.Infrastructure.Persistence;
using SGIRQ.Infrastructure.Repositories;
using SGIRQ.Infrastructure.Security;

namespace SGIRQ.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") 
                               ?? "Data Source=sgirq.db";

        services.AddDbContext<SgirqDbContext>(options =>
            options.UseSqlite(connectionString));

        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddSingleton<IPasswordHasher, BcryptPasswordHasher>();
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }
}