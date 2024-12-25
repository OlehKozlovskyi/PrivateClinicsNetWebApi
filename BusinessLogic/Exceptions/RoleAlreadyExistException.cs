using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinicsWebNet.BusinessLogic.Exceptions;

public class RoleAlreadyExistException : Exception
{
    public RoleAlreadyExistException(string roleName) 
        :base($"The role already exists in the system."){ }
}
