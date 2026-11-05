using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Sgirq.Application.Abstractions;
using Sgirq.Domain.Entities;
namespace Sgirq.Api.Security;
public sealed class JwtTokens(IConfiguration config) : ITokens
{
    public string Generate(Personal p)
    {
        var secret = config["Jwt:Key"] ?? throw new InvalidOperationException("Configura Jwt:Key");
        var claims = new[] {
            new Claim(ClaimTypes.NameIdentifier, p.IdPersonal.ToString()),
            new Claim(ClaimTypes.Name, p.UsuarioPersonalLogin),
            new Claim(ClaimTypes.Role, p.TipoPersonal)
        };
        var token = new JwtSecurityToken(claims: claims, expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)), SecurityAlgorithms.HmacSha256));
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
