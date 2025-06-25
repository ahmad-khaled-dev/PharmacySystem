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
            string? role = (await
            _userManager.GetRolesAsync(user)).FirstOrDefault();

            DateTime expirationToken =
                DateTime.UtcNow.AddMinutes(

                 //_configuration.GetValue<Double>("Jwt:ExpirationMintues") 
                 _jwtSettings.ExiprationMinutes

                 );


            DateTime exiprationRefreshToken = DateTime.UtcNow.AddMinutes(

                _configuration.GetValue<Double>("RefreshToken:ExpirationMintues")
                );


            //Info that would include in payload


            List<Claim> claims = new List<Claim>{

                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                new Claim(ClaimTypes.Name,user.UserName) ,
                new Claim(ClaimTypes.Role,role)


              };


            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken
                (
                   issuer: _jwtSettings.Issuer,
                   audience: _jwtSettings.Audience,
                   claims: claims.ToArray(),

                    expires: expirationToken,
                   signingCredentials: signingCredentials

                );


            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();


            string token = tokenHandler.WriteToken(jwtSecurityToken);


            return new AuthenticationResponse()
            {
                UserName = user.UserName,
                Token = token,
                ExiprationToken = expirationToken,
                RefreshToken = GenerateRefreshToken(),
                RefreshExpiration = exiprationRefreshToken,
                Role = role
            };

        }
         
        public ClaimsPrincipal? GetPrincipleFromJwtToken(string? token)
        {
            if (token == null)
            {
                return null;
            }

            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,

                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key!)),

                //should be false here

                ValidateLifetime = false

            };


            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            ClaimsPrincipal claimsPrincipal = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
       StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }


            return claimsPrincipal;

        }
         
        private string GenerateRefreshToken()
        {

            byte[] bytes = new byte[64];

            var randomNumberGenerate = RandomNumberGenerator.Create();

            randomNumberGenerate.GetBytes(bytes);


            return Convert.ToBase64String(bytes);


        }


    }
}
