using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nkatman.Service.Exceptions
{
    public class ClientSideException(string message) : Exception(message)
    {
    }
}
