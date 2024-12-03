using ExamplePredefinedAPIKey.Data;
using ExamplePredefinedAPIKey.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamplePredefinedAPIKey.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompaniesController : ControllerBase
    {
        private readonly EnterpriseDbContext _context;

        public CompaniesController(EnterpriseDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompanies()
        {
            var companies = await _context.Companies.AsNoTracking().ToListAsync();
            return Ok(companies);
        }

        [HttpPost]
        public async Task<ActionResult<Company>> AddCompany(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            return Ok(company);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompany(int id)
        {
            var company = await _context.Companies.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

            if(company == null) return NotFound();

            return Ok(company);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCompany(int id, Company editedCompany)
        {
            if(editedCompany.Id != id) return BadRequest();

            var companyCurrent = await _context.Companies.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

            if(companyCurrent == null) return NotFound();

            _context.Entry(editedCompany).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCompany(int id)
        {
            var company = await _context.Companies.FindAsync(id);

            if(company == null) return NotFound();

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
