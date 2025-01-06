using PrivateClinicsWebNet.Application.Wrapper;

namespace PrivateClinicsWebNet.Infrastructure.Migrator.Abstractions
{
    public interface IDataMigrationService
    {
        Task<Result<bool>> MigrateDataAsync(string path);
    }
}