using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nkatman.Core.DTOs.UpdateDTOs
{
    public class PaymentUpdateDto
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public double Amount { get; set; }
    }
}
