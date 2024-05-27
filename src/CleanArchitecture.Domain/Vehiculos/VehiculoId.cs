namespace CleanArchitecture.Domain.Vehiculos;

/// <summary>
/// Strong Id de Vehiculo
/// </summary>
public record VehiculoId(Guid Value)
{
    public static VehiculoId New() => new(Guid.NewGuid());
}