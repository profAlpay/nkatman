﻿using nkatman.Core.Models;
using nkatman.Core.Repositories;
using nkatman.Core.Services;
using nkatman.Core.UnitOfWorks;
using nkatman.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace nkatman.Service.Services
{
    public class GroupService : Service<Group>,IGroupService
    {
        public GroupService(IGenericRepository<Group> repository, IUnitOfWorks unitOfWorks) : base(repository, unitOfWorks)
        {
        }
    }
}
