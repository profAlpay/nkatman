using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nkatman.Core.Models
{
    public class Payment:BaseEntity
    {
        public int CustomerId { get; set; }

        public double Amount { get; set; }

        public customer Customer { get; set; }



    }
}
