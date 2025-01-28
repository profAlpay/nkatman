using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nkatman.Core.Models
{
    public class customer: BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Payment> Payments { get; set; }
        public ICollection<Sale> Sales { get; set; }
    }
}
