using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> RunMigration(string path)
        {
            await _migrationService.MigrateDataAsync(path);
            return Ok("Migration has completed successfully");
        }
    }
}
