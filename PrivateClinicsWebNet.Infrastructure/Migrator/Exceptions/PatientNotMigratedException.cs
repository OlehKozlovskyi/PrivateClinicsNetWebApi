namespace PrivateClinicsWebNet.Infrastructure.Migrator.Exceptions
{
    [Serializable]
    public class PatientNotMigratedException : Exception
    {
        public PatientNotMigratedException() 
            : base(){ }

    }
}