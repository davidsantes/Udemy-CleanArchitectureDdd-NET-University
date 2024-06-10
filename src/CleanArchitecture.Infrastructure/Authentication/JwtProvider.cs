using CleanArchitecture.Application.Abstractions.Authentication;
using CleanArchitecture.Domain.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchitecture.Infrastructure.Authentication;

public sealed class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _options;

    public JwtProvider(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public Task<string> Generate(User user)
    {
        // Crear los claims (reclamaciones) que se incluirán en el token (ID del usuario, email...)
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id!.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!.Value)
        };

        // Crear las credenciales de firma usando la clave secreta y el algoritmo de seguridad
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey!)),
            SecurityAlgorithms.HmacSha256
        );

        // Crear el token JWT con los parámetros especificados
        var token = new JwtSecurityToken(
            _options.Issuer,
            _options.Audience,
            claims,
            null,
            DateTime.UtcNow.AddDays(365),
            signingCredentials
            );

        // Serializar el token a una cadena JWT
        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return Task.FromResult<string>(tokenValue);
    }
}