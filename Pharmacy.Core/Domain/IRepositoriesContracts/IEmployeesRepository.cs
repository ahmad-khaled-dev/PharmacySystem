using Pharmacy.Core.Domain.Entities.IdentityEntities;

namespace Pharmacy.Core.Domain.IRepositoriesContracts
{
    public interface IEmployeesRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();

        Task<Employee?> GetByIdAsync(int id);

        Task<Employee?> GetByUserIdAsync(Guid userId);

        Task<Employee> AddEmployeeAsync(Employee employee);

        Task<bool> UpdateEmployeeAsync(Employee employee);

        Task<bool> DeleteAsync(int id);
    }
}
