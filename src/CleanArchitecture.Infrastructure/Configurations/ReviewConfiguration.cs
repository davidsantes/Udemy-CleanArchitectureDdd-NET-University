using CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Reviews;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Vehiculos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations;

/// <summary>
/// Configuración de mapeo de entidad para la entidad Review en la base de datos.
/// </summary>
internal sealed class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        // Configuración de la tabla y clave primaria
        builder.ToTable("reviews");
        builder.HasKey(review => review.Id);

        // Configuración de mapeo para la propiedad Rating
        builder.Property(review => review.Rating)
            .HasConversion(rating => rating.Value, value => Rating.Create(value).Value);

        // Configuración de mapeo para la propiedad Comentario
        builder.Property(review => review.Comentario)
            .HasMaxLength(200)
            .HasConversion(comentario => comentario.Value, value => new Comentario(value));

        // Configuración de la relación con la entidad Vehiculo (N-1)
        builder.HasOne<Vehiculo>()
            .WithMany()
            .HasForeignKey(review => review.VehiculoId);

        // Configuración de la relación con la entidad Alquiler (N-1)
        builder.HasOne<Alquiler>()
            .WithMany()
            .HasForeignKey(review => review.AlquilerId);

        // Configuración de la relación con la entidad User (N-1)
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(review => review.UserId);
    }
}