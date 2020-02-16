using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ExpertalSystem.Authorization
{
    public class JwtManager : IJwtManager
    {
        private readonly IConfiguration _configuration;
        public JwtManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(string id, string username, int expireMinutes = 20)
        {
            var symmetricKey = Convert.FromBase64String(_configuration.GetValue<string>("secret"));
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                 new Claim(ClaimTypes.Name, username),
                 new Claim(ClaimTypes.NameIdentifier, id)
                }),
                Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),
                Issuer = _configuration.GetValue<string>("issuer"),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(symmetricKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);

            return token;
        }
    }
}
