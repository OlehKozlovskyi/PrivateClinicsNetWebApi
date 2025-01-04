namespace PrivateClinicsWebNet.Infrastructure.Migrator.Abstractions
{
    public interface IDataMigrationService
    {
        Task MigrateDataAsync(string path);
    }
}