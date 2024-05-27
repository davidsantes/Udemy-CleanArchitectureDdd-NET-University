using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure;

/// <summary>
/// Clase que representa el contexto de la base de datos de la aplicación.
/// </summary>
public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
    private readonly IPublisher _publisher;

    /// <summary>
    /// Constructor de la clase ApplicationDbContext.
    /// </summary>
    /// <param name="options">Opciones de configuración del contexto de la base de datos.</param>
    /// <param name="publisher">Instancia del publicador MediatR.</param>
    public ApplicationDbContext(DbContextOptions options, IPublisher publisher) : base(options)
    {
        _publisher = publisher;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Recoge todas del proyecto que hereden de IEntityTypeConfiguration
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    /// <summary>
    /// Guarda los cambios en la base de datos y publica los eventos de dominio asociados.
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación opcional.</param>
    /// <returns>Número de entidades afectadas.</returns>
    public override async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default
        )
    {
        try
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            await PublishDomainEventsAsync();

            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException("Se ha producido una excepción por concurrencia", ex);
        }
    }

    /// <summary>
    /// Método para publicar los eventos de dominio asociados a las entidades modificadas en la base de datos.
    /// </summary>
    /// <returns>Tarea asincrónica.</returns>
    private async Task PublishDomainEventsAsync()
    {
        // Obtiene todas las entidades que tienen eventos de dominio pendientes
        var domainEvents = ChangeTracker
            .Entries<IEntity>() // Obtiene las entradas de todas las entidades que están siendo rastreadas por el contexto
            .Select(entry => entry.Entity) // Selecciona las entidades de las entradas
            .SelectMany(entity =>
            {
                var domainEvents = entity.GetDomainEvents(); // Para cada entidad, obtiene sus eventos de dominio pendientes
                entity.ClearDomainEvents(); // Limpia los eventos de dominio de la entidad para evitar que se publiquen múltiples veces
                return domainEvents; // Retorna los eventos de dominio pendientes
            }).ToList(); // Convierte los eventos de dominio en una lista

        // Publica cada evento de dominio pendiente
        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent); // Publica el evento de dominio
        }
    }
}