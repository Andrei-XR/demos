using ExampleMultiTenant.Data.Migration;
using Microsoft.AspNetCore.Mvc;

namespace ExampleMultiTenant.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MigrationController : Controller
    {
        private readonly DatabaseMigrator _migrator;

        public MigrationController(DatabaseMigrator migrator)
        {
            _migrator = migrator;
        }

        [HttpGet("Update DB")]
        public async Task<IActionResult> Run()
        {
            var databases = new List<string> { "Database1", "Database2", "Database3" };

            try
            {
                await _migrator.MigrateDatabaseAsync(databases);
                return Ok("Migrações aplicadas com sucesso em todas as bases.");
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
