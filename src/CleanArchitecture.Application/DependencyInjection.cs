using CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Application.Abstractions.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //Registramos en MediatR:
        //- Todos los objetos Query y sus respectivos QueryHandler.
        //- Todos los objetos Command y sus respectivos CommandHandler.
        //- Todos los los behaviours / inyectores. 
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        services.AddTransient<PrecioService>();

        return services;
    }
}