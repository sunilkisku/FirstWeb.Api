using FirstWeb.Api.EmployeeData;
using FirstWeb.Api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstWeb.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly MockEmployeeData dbContext;

        public EmployeesController(MockEmployeeData dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            return Ok(await dbContext.Employees.ToListAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var employee = await dbContext.Employees.FindAsync(id);

            if(employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployees(AddEmployee addEmployee)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Email = addEmployee.Email,
                FullName = addEmployee.FullName,
                Phone = addEmployee.Phone,

            };

            await dbContext.Employees.AddAsync(employee);
            await dbContext.SaveChangesAsync();

            return Ok(employee);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, UpdateEmployee updateEmployee)
        {
            var employee = await dbContext.Employees.FindAsync(id);
            
            if(employee != null)
            {
                employee.FullName = updateEmployee.FullName;
                employee.Phone = updateEmployee.Phone;
                employee.Email = updateEmployee.Email;

                await dbContext.SaveChangesAsync();

                return Ok(employee);
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await dbContext.Employees.FindAsync(id);

            if(employee != null)
            {
                dbContext.Remove(employee);
                dbContext.SaveChanges();
                return Ok(employee);
            }

            return NotFound();
        }
    }
}
