using FirstWeb.Api.Model;
using Microsoft.EntityFrameworkCore;
namespace FirstWeb.Api.EmployeeData
{
    public class MockEmployeeData :DbContext
    {
        public MockEmployeeData(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Employee>Employees { get; set; }
    }
}
