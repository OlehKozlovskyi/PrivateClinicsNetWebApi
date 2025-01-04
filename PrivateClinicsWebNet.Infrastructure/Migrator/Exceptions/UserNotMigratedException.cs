namespace PrivateClinicsWebNet.Infrastructure.Migrator.Exceptions
{
    [Serializable]
    public class UserNotMigratedException : Exception
    {
        public UserNotMigratedException() 
            : base(){ }

    }
}