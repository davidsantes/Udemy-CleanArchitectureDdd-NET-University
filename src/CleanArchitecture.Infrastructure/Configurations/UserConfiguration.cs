using CleanArchitecture.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations;

/// <summary>
/// Configuración de mapeo de entidad para la entidad User en la base de datos.
/// </summary>
internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Configuración de la tabla y clave primaria
        builder.ToTable("users");
        builder.HasKey(user => user.Id);
        builder.Property(user => user.Id)
            .HasConversion(userId => userId!.Value, value => new UserId(value));

        // Configuración de mapeo para la propiedad Nombre
        builder.Property(user => user.Nombre)
            .HasMaxLength(200)
            .HasConversion(nombre => nombre!.Value, value => new Nombre(value));

        // Configuración de mapeo para la propiedad Apellido
        builder.Property(user => user.Apellido)
            .HasMaxLength(200)
            .HasConversion(apellido => apellido!.Value, value => new Apellido(value));

        // Configuración de mapeo para la propiedad Email
        builder.Property(user => user.Email)
            .HasMaxLength(400)
            .HasConversion(email => email!.Value, value => new Email(value));

        // Configuración de índice único para la propiedad Email
        builder.HasIndex(user => user.Email).IsUnique();
    }
}