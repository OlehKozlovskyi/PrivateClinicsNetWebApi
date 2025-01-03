using PrivateClinicsWebNet.BusinessLogic.Entities;

namespace PrivateClinicsWebNet.Infrastructure.Migrator.Abstractions
{
    public interface IFileReader
    {
        List<Doctor> Read(string link);
    }
}