using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Pharmacy.Core.ConfiurationSettings;
using Pharmacy.Core.Domain.Entities.IdentityEntities;
using Pharmacy.Core.DTO;
using Pharmacy.Core.IServiceContracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Pharmacy.Core.Services
{
    public class JwtService : IJwtService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        public JwtService(IOptions<JwtSettings> options, IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _jwtSettings = options.Value;
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<AuthenticationResponse> createJwtToken(ApplicationUser user)
        {
            string? role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            DateTime expirationToken = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes);

            DateTime expirationRefreshToken = DateTime.UtcNow.AddMinutes(_configuration.GetValue<double>("RefreshToken:ExpirationMintues"));

            // Claims - ملاحظة تعديل Iat لتكون بصيغة Unix Time
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                new Claim(ClaimTypes.Role, role ?? string.Empty)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expirationToken,
                signingCredentials: signingCredentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            string token = tokenHandler.WriteToken(jwtSecurityToken);

            return new AuthenticationResponse()
            {
                UserName = user.UserName,
                Token = token,
                ExiprationToken = expirationToken,
                RefreshToken = GenerateRefreshToken(),
                RefreshExpiration = expirationRefreshToken,
                Role = role
            };
        }

        public ClaimsPrincipal? GetPrincipleFromJwtToken(string? token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,

                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),

                ValidateLifetime = true, // نفعّل التحقق من انتهاء صلاحية التوكن
                ClockSkew = TimeSpan.Zero // لتقليل التسامح الزمني (اختياري)
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            ClaimsPrincipal claimsPrincipal = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return claimsPrincipal;
        }

        private string GenerateRefreshToken()
        {
            var bytes = new byte[64];
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}
