using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RS.CommonLibrary.Model;
using RS.CommonLibrary.Security.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static RS.CommonLibrary.Constants.CommonConstants;

namespace RS.CommonLibrary.Security.Jwt
{
    public class JwtService : IJwtService
    {
        private readonly JwtSettings _jwtSettings;

        public JwtService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public string GenerateToken(UserDto user)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, ((USER_ROLE)user.Role).ToString()),
            new Claim("Role", user.Role.ToString()) // Add numeric role for easy parsing
        };

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)),
                    SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                ClockSkew = TimeSpan.Zero
            };

            return tokenHandler.ValidateToken(token, tokenValidationParameters, out _);
        }

        public string GetUserIdFromToken(string token)
        {
            var principal = ValidateToken(token);
            return principal.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public IEnumerable<string> GetUserRolesFromToken(string token)
        {
            var principal = ValidateToken(token);
            return principal.FindAll(ClaimTypes.Role).Select(c => c.Value);
        }
    }
}
