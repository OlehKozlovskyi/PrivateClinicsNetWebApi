using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinicsWebNet.DataAccess.Exceptions
{
    public class RoleAlreadyExistException : Exception
    {
        public RoleAlreadyExistException(string roleName) 
            :base($"Role {roleName} already exist."){ }
    }
}
