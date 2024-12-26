namespace PrivateClinicsWebNet.Application.Exceptions
{
    [Serializable]
    public class InvalidUserRoleException : Exception
    {
        public InvalidUserRoleException(string roleName)
            :base($"Invalid user role: {roleName}") { }
    }
}