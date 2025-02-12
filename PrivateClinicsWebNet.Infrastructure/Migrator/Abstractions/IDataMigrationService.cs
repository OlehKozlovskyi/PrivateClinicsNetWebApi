using PrivateClinicsWebNet.Infrastructure.Shared.Wrapper;

namespace PrivateClinicsWebNet.Infrastructure.Migrator.Abstractions
{
    public interface IDataMigrationService
    {
        Task<Result<string>> MigrateDataAsync(string path);
    }
}