using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nkatman.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        Task<T> GetByIdAsync(int id);

        int Count();

        void Update(T entity);

        void ChangeStatus(T entity);

        Task AddAsync(T entity);



    }
}
