using EMSystem.Data;
using EMSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EMSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EMSDbContext context;

        public EmployeeController(EMSDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Employee>>> Get(int id)
        {
            var employees = await context.Employees.
                Where(c => c.EmployeeId == id).
                Include(c => c.Department).
                FirstOrDefaultAsync();
            if (employees == null)
            {
                return NotFound();
            }
            return Ok(employees);
        }
        [HttpPost]
        public async Task<ActionResult<List<Employee>>> create(CreateEmployeeDto request)
        {
            var department = await context.Departments.FindAsync(request.DepartmentId);
            if (department == null)
                return NotFound();
            var newEmployee = new Employee
            {
                Name = request.Name,
                Adress = request.Adress,
                Department = department
            };
            context.Employees.Add(newEmployee);
            await context.SaveChangesAsync();
            return await Get(newEmployee.EmployeeId);
            
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Employee>>> GetCount(int id)
        {
            var countlist = await context.Employees.
                Where(c => c.DepartmentId == id).
                
  
            ToListAsync();
            int c = countlist.Count();
            return Ok(c);
        }
    }
}
