using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinicsWebNet.Application.Exceptions;

public class FailedCreateRoleOperationException : Exception
{
    public FailedCreateRoleOperationException()
        :base(){ }
}
