using nkatman.Core.Models;
using nkatman.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace nkatman.Service.Services
{
    public class TokenHandler : ITokenHandler
    {
        public string CreateRefreshToken()
        {
            throw new NotImplementedException();
        }

        public Token CreateToken(User user, List<Role> roles)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Claim> SetClaims(User user, List<Role> roles)
        {
            throw new NotImplementedException();
        }
    }
}
