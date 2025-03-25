using nkatman.Core.DTOs;
using nkatman.Core.Models;
using nkatman.Core.Repositories;
using nkatman.Core.Services;
using nkatman.Core.UnitOfWorks;
using nkatman.Service.Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nkatman.Service.Services
{
    public class UserService : Service<User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenHandler _tokenHandler;


        public UserService(IGenericRepository<User> repository, IUnitOfWorks unitOfWorks, IUserRepository userRepository, ITokenHandler tokenHandler) : base(repository, unitOfWorks)
        {
            _userRepository = userRepository;
            _tokenHandler = tokenHandler;
        }

        public User GetByEmail(string email)
        {
            User user = _userRepository.Where(x => x.Email == email).FirstOrDefault();

            return user ?? user;
        }

        public async Task<Token> Login(UserLoginDto userLoginDto)
        {
            var user = GetByEmail(userLoginDto.Email);

            if (user == null)
            {
                return null;
            }
            var result = false;

            result = HashingHelper.VerifyPasswordHash(userLoginDto.Password, user.PasswordHash, user.PasswordSalt);

            List<Role> roles = new List<Role>();

            // get roles TODO

            if (result)
            {
                Token token = _tokenHandler.CreateToken(user, roles);
                return token;
            }
            return null;

        }

    }
}
