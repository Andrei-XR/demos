using ExampleMultiTenant.Data;
using ExampleMultiTenant.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExampleMultiTenant.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly DynamicDbContext _context;

        public EmployeeController(DynamicDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await _context.Employees.ToListAsync();
            return Ok(employees);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody]Employee employee)
        {
            var user = new User
            {
                Email = "user@email.com",
                Name = employee.Name,
                Password = employee.Name.Substring(0, 3)
            };

            employee.User = user;
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return Ok(employee);
        }
    }
}
