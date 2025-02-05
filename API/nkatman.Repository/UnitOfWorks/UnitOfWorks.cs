using nkatman.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nkatman.Repository.UnitOfWorks
{
    public class UnitOfWorks(AppDbContext context) : IUnitOfWorks
    {
        private readonly AppDbContext _context=context;
        public void Commit()
        {
            _context.SaveChanges();
        }
        public async Task CommitAsync()
        {
           await _context.SaveChangesAsync();
        }

        
    }
}
