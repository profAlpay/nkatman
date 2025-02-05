﻿using nkatman.Core.Models;
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
    public class RoleService(IGenericRepository<Role> repository, IUnitOfWorks unitOfWorks) : Service<Role>(repository, unitOfWorks), IRoleService
    {

    }
}
