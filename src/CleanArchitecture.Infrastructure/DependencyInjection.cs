using CleanArchitecture.Application.Abstractions.Clock;
using CleanArchitecture.Application.Abstractions.Email;
using CleanArchitecture.Infrastructure.Clock;
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

        return services;
    }
}