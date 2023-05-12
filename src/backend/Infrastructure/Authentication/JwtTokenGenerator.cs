using Application.Interfaces.Authentication;
using Application.Interfaces.Services;
using Domain;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Authentication
{
    /// <summary>
    /// Jwt Token Generator class.
    /// </summary>
    public sealed class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly JwtSettings jwtSettings;

        public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtOptions)
        {
            this.dateTimeProvider = dateTimeProvider;
            jwtSettings = jwtOptions.Value;
        }

        public string GenerateToken(User user)
        {
            SigningCredentials signingCredentials = new(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                SecurityAlgorithms.HmacSha256);

            Claim[] claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            JwtSecurityToken secToken = new(
                issuer: jwtSettings.Issuer,
                expires: dateTimeProvider.UtcNow().AddMinutes(jwtSettings.ExpiryMinutes),
                claims: claims,
                signingCredentials: signingCredentials,
                audience: jwtSettings.Audience);

            return new JwtSecurityTokenHandler().WriteToken(secToken);
        }
    }
}
