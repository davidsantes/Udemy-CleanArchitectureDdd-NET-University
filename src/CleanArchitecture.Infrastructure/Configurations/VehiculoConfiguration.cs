using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Vehiculos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations;

/// <summary>
/// Configuración de mapeo de entidad para la entidad Vehiculo en la base de datos.
/// </summary>
internal sealed class VehiculoConfiguration : IEntityTypeConfiguration<Vehiculo>
{
    public void Configure(EntityTypeBuilder<Vehiculo> builder)
    {
        // Configuración de la tabla y clave primaria
        builder.ToTable("vehiculos");
        builder.HasKey(vehiculo => vehiculo.Id);
        builder.Property(vehiculo => vehiculo.Id)
            .HasConversion(vehiculoId => vehiculoId!.Value, value => new VehiculoId(value));

        // Configuración de mapeo para la propiedad Direccion (1-1)
        builder.OwnsOne(vehiculo => vehiculo.Direccion);

        // Configuración de mapeo para la propiedad Modelo (1-1)
        builder.Property(vehiculo => vehiculo.Modelo)
            .HasMaxLength(200)
            .HasConversion(modelo => modelo!.Value, value => new Modelo(value));

        // Configuración de mapeo para la propiedad Vin (1-1)
        builder.Property(vehiculo => vehiculo.Vin)
            .HasMaxLength(500)
            .HasConversion(vin => vin!.Value, value => new Vin(value));

        // Configuración de mapeo para la propiedad Precio (1-1)
        builder.OwnsOne(vehiculo => vehiculo.Precio, priceBuilder =>
        {
            priceBuilder.Property(moneda => moneda.TipoMoneda)
            .HasConversion(tipoMoneda => tipoMoneda.Codigo, codigo => TipoMoneda.FromCodigo(codigo!));
        });

        // Configuración de mapeo para la propiedad Mantenimiento (1-1)
        builder.OwnsOne(vehiculo => vehiculo.Mantenimiento, priceBuilder =>
        {
            priceBuilder.Property(moneda => moneda.TipoMoneda)
            .HasConversion(tipoMoneda => tipoMoneda.Codigo, codigo => TipoMoneda.FromCodigo(codigo!));
        });

        // Configuración de mapeo para la propiedad Version como una columna de versión en la base de datos para control de concurrencia optimista
        builder.Property<uint>("Version").IsRowVersion();
    }
}