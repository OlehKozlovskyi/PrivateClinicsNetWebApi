using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinicsWebNet.BusinessLogic.Entities
{
    public static class RoleRegistry
    {
        public static readonly IdentityRole Admin = new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" };
        public static readonly IdentityRole Doctor = new IdentityRole { Id = "2", Name = "Doctor", NormalizedName = "DOCTOR" };
        public static readonly IdentityRole Patient = new IdentityRole { Id = "3", Name = "Patient", NormalizedName = "PATIENT" };

        public static IReadOnlyList<IdentityRole> GetRoles()
        {
            Type type = typeof(RoleRegistry);
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Static);
            var roles = new List<IdentityRole>();
            foreach (FieldInfo field in fields)
            {
                var role = (IdentityRole)field.GetValue(null);
                roles.Add(role);
            }
            return roles;
        }
    }
}
