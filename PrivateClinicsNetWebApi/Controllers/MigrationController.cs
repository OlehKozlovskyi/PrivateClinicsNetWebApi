using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrivateClinicsNetWebApi.Extensions;
using PrivateClinicsWebNet.Infrastructure.Migrator.Abstractions;

namespace PrivateClinicsNetWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MigrationController : ControllerBase
    {
        private readonly IDataMigrationService _migrationService;

        public MigrationController(IDataMigrationService migrationService) 
        {
            _migrationService = migrationService;    
        }

        [HttpPost("migrate-data")]
        public async Task<IActionResult> RunMigration(string path = @"C:/Users/OlehKozlovskyi/Documents/GitHub/test3.json")
        {
            var result = await _migrationService.MigrateDataAsync(path);
            return result.ToResponse();
        }
    }
}
