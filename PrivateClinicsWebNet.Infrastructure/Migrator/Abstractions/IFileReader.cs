using PrivateClinicsWebNet.BusinessLogic.Entities;

namespace PrivateClinicsWebNet.Infrastructure.Migrator.Abstractions
{
    public interface IFileReader
    {
        IEnumerable<Doctor> Read(string link);
    }
}