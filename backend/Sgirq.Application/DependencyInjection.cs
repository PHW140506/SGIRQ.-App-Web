using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Sgirq.Application.Common;
namespace Sgirq.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<Sgirq.Application.Abstractions.IMapper, EntityMapper>();
        services.AddScoped<Sgirq.Application.Abstractions.IReactivoRules, ReactivoRules>();
        services.AddScoped<Sgirq.Application.Abstractions.ISalidaProyeccion, SalidaProyeccion>();
        services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        return services;
    }
}
