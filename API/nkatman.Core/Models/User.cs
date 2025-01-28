using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nkatman.Core.Models
{
    public class User:BaseEntity
    {
        public string Name { get; set; }

        public int DepartmentId { get; set; }

        public int GroupId { get; set; }

        public byte[] PasswordSalt { get; set; }

        public byte[] PasswordHash { get; set; }
    }
}
