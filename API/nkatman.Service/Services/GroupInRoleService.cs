using nkatman.Core.Models;
using nkatman.Core.Repositories;
using nkatman.Core.Services;
using nkatman.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nkatman.Service.Services
{
    public class GroupInRoleService : Service<GroupInRole>, IGroupInRoleService
    {
        public GroupInRoleService(IGenericRepository<GroupInRole> repository, IUnitOfWorks unitOfWorks) : base(repository, unitOfWorks)
        {
        }
    }
}
