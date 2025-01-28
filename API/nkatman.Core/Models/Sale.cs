using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nkatman.Core.Models
{
    public class Sale: BaseEntity
    {
        public int CustomerId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public double UnitPrice { get; set; }

        public double   TotalPrice { get; set; }

       
        public customer customer { get; set; }

        public Product Product { get; set; }
    }
}
