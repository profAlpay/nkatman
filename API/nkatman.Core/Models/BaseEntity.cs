using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace nkatman.Core.Models
{
    public abstract class BaseEntity
    {
        public int  Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public int CreatedBy { get; set; }

        public int UpdateBy { get; set; }

        public bool Status { get; set; }
    }

}
