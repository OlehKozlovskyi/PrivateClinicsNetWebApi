using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinicsWebNet.Application.Exceptions
{
    public class InvalidAppointmentsIdException : Exception
    {
        public InvalidAppointmentsIdException()
            :base(){ }
    }
}
