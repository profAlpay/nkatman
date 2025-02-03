using nkatman.Core.Models;
using nkatman.Core.Repositories;
using nkatman.Repository.Repositories;
using nkatman.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nkatman.Repository.Repositories
{
    public class UserRepository(AppDbContext context) : GenericRepository<User>(context), IUserRepository
    {
    
    
    }
}

