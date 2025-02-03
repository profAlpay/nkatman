﻿using nkatman.Core.Models;
using nkatman.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nkatman.Repository.Repositories
{
    public class GrupInRoleRepository(AppDbContext context):GenericRepository<GroupInRole>(context), IGroupInRoleRepository
    {
    }
}
