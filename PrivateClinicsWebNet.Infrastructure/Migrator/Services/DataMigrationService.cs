using Newtonsoft.Json;
using PrivateClinicsWebNet.BusinessLogic.Abstractions;
using PrivateClinicsWebNet.BusinessLogic.Entities;
using PrivateClinicsWebNet.DataAccess;
using PrivateClinicsWebNet.Infrastructure.Migrator.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateClinicsWebNet.Infrastructure.Services
{
    public class DataMigrationService
    {
        private readonly IFileReader _reader;
        private readonly IUserRepository _userRepository;
        private readonly IUserFactory _userFactory;
        private readonly ApplicationDbContext _dbContext;

        public DataMigrationService(
            IFileReader fileReader, 
            IUserRepository userRepository, 
            IUserFactory userFactory,
            ApplicationDbContext applicationDbContext
            ) 
        {
            _reader = fileReader;
            _userRepository = userRepository;
            _userFactory = userFactory;
            _dbContext = applicationDbContext;
        }

        public void MigrateData(string link)
        {

        }
    }
}
