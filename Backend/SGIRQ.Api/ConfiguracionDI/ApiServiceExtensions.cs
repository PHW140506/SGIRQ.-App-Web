using SGIRQ.Application;
using SGIRQ.Infrastructure;

namespace SGIRQ.Api.ConfiguracionDI;

public static class ApiServiceExtensions
{
    public static IServiceCollection AddSgirqDependencies(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddApplicationServices();
        services.AddInfrastructureServices(configuration);

        services.AddCors(options =>
        {
            options.AddPolicy("AngularClientPolicy", policy =>
                policy.WithOrigins("http://localhost:4200")
                      .AllowAnyHeader()
                      .AllowAnyMethod());
        });

        return services;
    }
}