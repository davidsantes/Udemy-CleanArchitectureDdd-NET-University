namespace CleanArchitecture.Domain.Alquileres;

/// <summary>
/// Strong Id de Vehiculo
/// </summary>
public record AlquilerId(Guid Value)
{
    public static AlquilerId New() => new(Guid.NewGuid());
}