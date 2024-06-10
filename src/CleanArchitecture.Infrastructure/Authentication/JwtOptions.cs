namespace CleanArchitecture.Infrastructure.Authentication;

public class JwtOptions
{
    public string? SecretKey { get; set; }
    public string? Audience { get; set; }
    public string? Issuer { get; set; }
}