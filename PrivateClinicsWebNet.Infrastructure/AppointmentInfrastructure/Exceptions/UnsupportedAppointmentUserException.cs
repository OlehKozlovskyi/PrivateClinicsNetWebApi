using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinicsWebNet.Infrastructure.AppointmentInfrastructure.Exceptions
{
    public class UnsupportedAppointmentUserException:Exception
    {
        public UnsupportedAppointmentUserException(string message)
            : base(message) { }
    }
}
