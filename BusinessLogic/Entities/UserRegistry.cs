using Microsoft.AspNetCore.Identity;
using PrivateClinicsWebNet.BusinessLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinicsWebNet.BusinessLogic.Entities
{
    public static class UserRegistry
    {
        private static readonly IReadOnlyList<Type> _derivedUserTypes;

        static UserRegistry()
        {
            _derivedUserTypes = GetIdentityUserSubclasses();
        }

        public static IReadOnlyCollection<Type> GetUserTypes() => _derivedUserTypes;

        public static Type GetUserTypeByName(string userType)
        {
            var returnedType = _derivedUserTypes.FirstOrDefault(x => x.Name == userType);
            if (returnedType == null)
                throw new InvalidUserTypeException();
            return returnedType;
        }

        public static IReadOnlyList<Type> GetIdentityUserSubclasses()
        {
            var subclassesList = new List<Type>();
            var types = Assembly.GetExecutingAssembly().GetTypes();
            var subclasses = types.Where(type => IsTypeIdentityUserSubclass(type)).ToList();
            subclassesList.AddRange(subclasses);
            return subclassesList;
        }

        private static bool IsTypeIdentityUserSubclass(Type type)
        {
            return type.IsClass && (!type.IsAbstract && type.IsSubclassOf(typeof(IdentityUser)));
        }
    }
}
