using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using NexusService.Models;

using NexusService.Service.IService;

namespace NexusService.Service
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _symmetricSecurityKey;
        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
            _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtOptions:Secret"]));
        }

        public string GenerateToken(ApplicationUser user, IEnumerable<string> roles)
        {
            var claimsList = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.DisplayName),
                new Claim(ClaimTypes.NameIdentifier, user.Id)

                
              
            };
            claimsList.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var credential = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claimsList),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = credential,
                Issuer = _configuration["JwtOptions:Issuer"],
                Audience = _configuration["JwtOptions:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
