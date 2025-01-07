using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebMigrationsBack.Interfaces;

namespace WebMigrationsBack.Services
{
    public class JWTokenService : IJWTokenService
    {
        public async Task<string> GenerateJwtToken(string email)
        {
            var secretKey = "G7&sh3*2JkL9z1@QrV8#oP$yM4e^NtX%WjHuA!kBb";
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken
            (
                issuer: "http://localhost:5033/",
                audience: "http://localhost:4200/",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}