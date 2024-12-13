using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinicsWebNet.DataAccess.Exceptions
{
    public class InvalidPasswordException : Exception
    {
        public InvalidPasswordException()
            :base("Invalid password exception!"){ }
    }
}
