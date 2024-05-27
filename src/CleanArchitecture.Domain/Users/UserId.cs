namespace CleanArchitecture.Domain.Users;

/// <summary>
/// Strong Id de User
/// </summary>
public record UserId(Guid Value)
{
    public static UserId New() => new(Guid.NewGuid());
}