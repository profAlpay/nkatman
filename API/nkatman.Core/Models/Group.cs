using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nkatman.Core.Models
{
    public class Group : BaseEntity

    {
        public string Name { get; set; }

        public ICollection<User> users { get; set; }

        

        public ICollection<GroupInRole> GroupInRols { get; set; }

    }
}
