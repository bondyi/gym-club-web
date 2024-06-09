using API.Core.Models;
using API.Core.Models.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace API.Helpers
{
    public class TokenHelper(IConfiguration configuration)
    {
        private readonly JwtSettings? _jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>();

        public string GenerateAccessToken(User user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new(ClaimTypes.Role, user.UserRole)
            };

            if (_jwtSettings == null) throw new NullReferenceException("JWT configuration not found.");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(_jwtSettings.TokenExpirationInDays),
                SigningCredentials = credentials,
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }

        public string GenerateRefreshToken()
        {
            var token =  Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            return token.Replace('+', 'z');
        }
    }
}
