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
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Numerics;
using PrivateClinicsWebNet.Application.Wrapper;

namespace PrivateClinicsWebNet.Infrastructure.Migrator.Services
{
    public class DataMigrationService : IDataMigrationService
    {
        private readonly IFileReader _reader;
        private readonly IUserRepository _userRepository;
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly UserMigrationOptions _defaultUsersSettings;

        public DataMigrationService(
            IFileReader fileReader,
            IUserRepository userRepository,
            ApplicationDbContext applicationDbContext,
            ILogger<DataMigrationService> logger,
            IOptions<UserMigrationOptions> userSettings
            )
        {
            _reader = fileReader;
            _userRepository = userRepository;
            _dbContext = applicationDbContext;
            _logger = logger;
            _defaultUsersSettings = userSettings.Value;
        }

        public async Task<Result<string>> MigrateDataAsync(string path)
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                foreach (var migrationData in _reader.Read(path))
                {
                    await MigratePatientsAsync(migrationData.PatientsList);
                    await MigrateDoctorAsync(migrationData.Doctor);
                }
                await transaction.CommitAsync();
                _logger.LogInformation("Migration has completed successfully", DateTime.UtcNow.ToLongTimeString());
                return await Result<string>.SuccessAsync("Migration has completed successfully");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Data migration operation has failed!", DateTime.UtcNow.ToLongTimeString());
                return await Result<string>.FailureAsync("Data migration operation has failed!");
            }
        }

        private async Task<Result<bool>> MigratePatientsAsync(List<Patient> patients)
        {
            foreach (var patient in patients)
            {
                if (!IsTracked(patient))
                {
                    var registerPatientResult = await _userRepository.RegisterUserAsync(patient, _defaultUsersSettings.DefaultPassword);
                    if (!registerPatientResult.Succeeded)
                    {
                        throw new PatientNotMigratedException();
                    }
                    await _userRepository.AddToRoleAsync(patient, nameof(Patient));
                }
            }
            return await Result<bool>.SuccessAsync();
        }

        private async Task<Result<bool>> MigrateDoctorAsync(Doctor doctor)
        {
            if (!IsTracked(doctor))
            {
                var result = await _userRepository.RegisterUserAsync(doctor, _defaultUsersSettings.DefaultPassword);
                if (!result.Succeeded)
                {
                    throw new DoctorNotMigratedException();
                }
                await _userRepository.AddToRoleAsync(doctor, nameof(Doctor));
            }
            return await Result<bool>.SuccessAsync();
        }

        private bool IsTracked(IdentityUser user)
        {
            HashSet<string> users = _dbContext.Users.Local
                .Select(x => x.Id)
                .ToHashSet<string>();
            return users.Contains(user.Id);
        }
    }
}
