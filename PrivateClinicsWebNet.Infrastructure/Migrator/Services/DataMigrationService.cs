using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PrivateClinicsWebNet.BusinessLogic.Abstractions;
using PrivateClinicsWebNet.BusinessLogic.Entities;
using PrivateClinicsWebNet.DataAccess;
using PrivateClinicsWebNet.Infrastructure.Migrator.Abstractions;
using PrivateClinicsWebNet.Infrastructure.Migrator.Models;
using PrivateClinicsWebNet.Infrastructure.Migrator.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinicsWebNet.Infrastructure.Migrator.Services
{
    public class DataMigrationService : IDataMigrationService
    {
        private readonly IFileReader _reader;
        private readonly IUserRepository _userRepository;
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly UserMigrationDefaults _defaultUsersSettings;

        public DataMigrationService(
            IFileReader fileReader,
            IUserRepository userRepository,
            ApplicationDbContext applicationDbContext,
            ILogger<DataMigrationService> logger,
            IOptions<UserMigrationDefaults> userSettings
            )
        {
            _reader = fileReader;
            _userRepository = userRepository;
            _dbContext = applicationDbContext;
            _logger = logger;
            _defaultUsersSettings = userSettings.Value;
        }

        public async Task MigrateDataAsync(string path)
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                foreach (var doctor in _reader.Read(path))
                {
                    var result = await _userRepository.RegisterUserAsync(doctor, _defaultUsersSettings.DefaultPassword);
                    if (!result.Succeeded)
                    {
                        throw new UserNotMigratedException();
                    }
                    await _userRepository.AddToRoleAsync(doctor, nameof(Doctor));
                    await transaction.CommitAsync();
                    _logger.LogInformation("Migration has completed successfully", DateTime.UtcNow.ToLongTimeString());
                }
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Data migration operation was failed!", DateTime.UtcNow.ToLongTimeString());
            }
        }
    }
}
