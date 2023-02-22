using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using serverSite.Entities;
using serverSite.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace serverSite.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }
        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId,user.UserName)
            };

            var cred = new SigningCredentials(_key,SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = new DateTime().AddDays(2),
                SigningCredentials = cred
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

         public string CreateToken(AppUser user,string tokenKey)
        {
            var securityToken = new JwtSecurityTokenHandler().CreateToken
            (
                new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(
                        new List<Claim>
                        {
                            new Claim(JwtRegisteredClaimNames.NameId,user.UserName)
                        }
                    ),
                    Expires = new DateTime().AddDays(2),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
                        SecurityAlgorithms.HmacSha512Signature
                    )
                }
            );
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}