namespace PrivateClinicsWebNet.Application.Exceptions
{
    public class InvalidUserIdException : Exception
    {
        public InvalidUserIdException(string? message) 
            : base(message){}
    }
}