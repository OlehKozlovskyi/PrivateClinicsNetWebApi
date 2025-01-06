using Microsoft.EntityFrameworkCore.Migrations;
using PrivateClinicsWebNet.Application.Wrapper;
using PrivateClinicsWebNet.BusinessLogic.Entities;
using PrivateClinicsWebNet.Infrastructure.Migrator.DTOs;

namespace PrivateClinicsWebNet.Infrastructure.Migrator.Abstractions
{
    public interface IFileReader
    {
        IEnumerable<MigrationDto> Read(string link);
    }
}