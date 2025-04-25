using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nkatman.Core.Models
{
    public class Product: BaseEntity

    {
        public string Name { get; set; }

        public double UnitPrice { get; set; }

        public int Stock { get; set; }

        public ICollection<Sale> Sales { get; set; }


    }
}
