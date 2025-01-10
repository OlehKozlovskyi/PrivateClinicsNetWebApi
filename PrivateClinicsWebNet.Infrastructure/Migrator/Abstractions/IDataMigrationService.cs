using PrivateClinicsWebNet.Application.Wrapper;

namespace PrivateClinicsWebNet.Infrastructure.Migrator.Abstractions
{
    public interface IDataMigrationService
    {
        Task<Result<string>> MigrateDataAsync(string path);
    }
}