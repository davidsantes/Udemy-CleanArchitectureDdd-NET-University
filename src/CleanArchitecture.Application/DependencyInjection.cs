using CleaArchitecture.Domain.Alquileres;
using Microsoft.Extensions.DependencyInjection;

namespace CleaArchitecture.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            //Inyectamos:
            //- todos los objetos Query y sus respectivos QueryHandler.
            //- todos los objetos Command y sus respectivos CommandHandler.
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });

        services.AddTransient<PrecioService>();

        return services;
    }
}