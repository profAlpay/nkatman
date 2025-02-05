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
    public class DepartmentService : Service<Department>, IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentService(IGenericRepository<Department> repository, IUnitOfWorks unitOfWorks, IDepartmentRepository departmentRepository) : base(repository, unitOfWorks)
        {
            _departmentRepository = departmentRepository;
        }
    }
}
