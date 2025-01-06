namespace PrivateClinicsWebNet.Infrastructure.Migrator.Exceptions
{
    [Serializable]
    internal class DoctorNotMigratedException : Exception
    {
        public DoctorNotMigratedException()
            : base(){ }

    }
}