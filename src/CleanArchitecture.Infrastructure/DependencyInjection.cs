using CleanArchitecture.Application.Abstractions.Clock;
using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Application.Abstractions.Email;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Vehiculos;
using CleanArchitecture.Infrastructure.Clock;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Infrastructure.Repositories;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
        )
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        services.AddTransient<IEmailService, EmailService>();

        var connectionString = configuration.GetConnectionString("Database")
             ?? throw new ArgumentNullException(nameof(configuration));

        // Configuración del contexto de la base de datos utilizando Npgsql (PostgreSQL) y convenciones de nomenclatura SnakeCase.
        // Las convenciones de nomenclatura SnakeCase se refieren a un estilo de escritura de identificadores donde las palabras se separan por guiones bajos (_). Por ejemplo, en SnakeCase, una frase como "nombreDeUsuario"
        // se escribiría como "nombre_de_usuario".
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
        });

        // Registro de implementaciones de repositorios como servicios de ámbito:
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IVehiculoRepository, VehiculoRepository>();
        services.AddScoped<IAlquilerRepository, AlquilerRepository>();

        // Registro de implementación de IUnitOfWork como servicio de ámbito, utilizando el contexto de la base de datos
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        // Registro de implementación de ISqlConnectionFactory como servicio singleton, utilizando la cadena de conexión a la base de datos
        // Esta co
        services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));

        // Registro de un manejador de tipo personalizado para Dapper para el tipo DateOnly:
        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

        return services;
    }
}