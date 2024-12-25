namespace PrivateClinicsWebNet.Application.Exceptions
{
    [Serializable]
    internal class InvalidUserRoleException : Exception
    {
        public InvalidUserRoleException()
        {
        }

        public InvalidUserRoleException(string? message) : base(message)
        {
        }

        public InvalidUserRoleException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}