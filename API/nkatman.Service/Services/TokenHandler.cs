using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using nkatman.Core.Models;
using nkatman.Core.Services;
using nkatman.Service.Extensions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace nkatman.Service.Services
{
    public class MyTokenHandler : ITokenHandler
    {
        private readonly IConfiguration _Configuration;

        public MyTokenHandler(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        public string CreateRefreshToken()
        {
           byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            {
                random.GetBytes(number);
                return Convert.ToBase64String(number);
            }
        }

        public Token CreateToken(User user, List<Role> roles)
        {
            Token token = new Token();

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Configuration["Token:SecurityKey"]));

            SigningCredentials signingCredentials = new(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            token.Expiration = DateTime.Now.AddDays(7);

            JwtSecurityToken jwtSecurityToken = new(
                issuer: _Configuration["Token:Issuer"],
                audience: _Configuration["Token:Audience"],
                claims: SetClaims(user, roles),
                expires: token.Expiration,
                notBefore: DateTime.Now,
                signingCredentials: signingCredentials
                );

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();

            token.AccessToken = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);

            token.RefreshToken = CreateRefreshToken();

            return token;
        }

        public IEnumerable<Claim> SetClaims(User user, List<Role> roles)
        {
            Claim claim = new("sub", user.Id.ToString());
            List<Claim> claims = new List<Claim>();
            {

                claims.Add(claim);
                claims.AddName(user.Name);
                claims.AddRoles(roles.Select(p=>p.Name).ToArray());

                return claims;
            }
            ;
        }
    }
}
