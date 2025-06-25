using Microsoft.EntityFrameworkCore;
using Pharmacy.Core.Domain.Entities.IdentityEntities;
using Pharmacy.Core.Domain.IRepositoriesContracts;
using Pharmacy.Infrastructure.DbContext;

namespace Pharmacy.Infrastructure.Repositories
{
    public class EmployeesRepositoy : IEmployeesRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Employee> _employees;

        public EmployeesRepositoy(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _employees = dbContext.Employees;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _employees.Include(e => e.User).Order().ToListAsync();
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));

            await _employees.AddAsync(employee);
            await _dbContext.SaveChangesAsync();

            return employee;
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
             

            return await _employees.Include(e => e.User)
                .FirstOrDefaultAsync(e => e.EmployeeID == id);
        }

        public async Task<Employee?> GetByUserIdAsync(Guid userId)
        {
             
            return await _employees.Include(e => e.User)
                .FirstOrDefaultAsync(e => e.UserId == userId);
        }

        public async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
             
            var existingEmployee = await _employees.FindAsync(employee.EmployeeID);
            if (existingEmployee == null) return false;


            _employees .Entry(existingEmployee).CurrentValues.SetValues(employee);
            await _dbContext.SaveChangesAsync();
              
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {

            var employee = await _employees.FindAsync(id);
            if (employee == null) return false;

            _employees.Remove(employee);
            await _dbContext.SaveChangesAsync();
            return true;

        }
    }
}
