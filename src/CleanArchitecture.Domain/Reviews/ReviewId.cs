namespace CleanArchitecture.Domain.Reviews;

/// <summary>
/// Strong Id de Vehiculo
/// </summary>
public record ReviewId(Guid Value)
{
    public static ReviewId New() => new(Guid.NewGuid());
}