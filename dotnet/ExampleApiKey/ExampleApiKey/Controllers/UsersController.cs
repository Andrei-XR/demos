using ExampleApiKey.Data;
using ExampleApiKey.Models;
using ExampleApiKey.Utils;
using Microsoft.AspNetCore.Mvc;

namespace ExampleApiKey.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly EnterpriseDbContext _context;

        public UsersController(EnterpriseDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(User user)
        {
            user.ApiKey = ApiKeyGenerator.GenerateApiKey();
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { user.Id, user.Name, user.ApiKey });
        }
    }
}
