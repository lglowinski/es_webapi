using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ExpertalSystem.Authorization
{
    public class JWTManager : IJWTManager
    {
        private readonly IConfiguration _configuration;
        public JWTManager(IConfiguration configuration)
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
