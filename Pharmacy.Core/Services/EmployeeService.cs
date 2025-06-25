using Microsoft.AspNetCore.Identity;
using Pharmacy.Core.Domain.Entities.IdentityEntities;
using Pharmacy.Core.Domain.IRepositoriesContracts;
using Pharmacy.Core.DTO.EmployeeDTO;

using Pharmacy.Core.IServiceContracts;

namespace Pharmacy.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeesRepository _employeesRepository;

        public EmployeeService(IEmployeesRepository employeesRepository)
        {
            _employeesRepository = employeesRepository;
        }

        public async Task<EmployeeResponse> CreateAsync(EmployeeAddRequest Employeerequest)
        {
            if (Employeerequest == null )
            {
                throw new ArgumentNullException();
            }
             
            var employee = Employeerequest.ToEmployee();
 
            await _employeesRepository.AddEmployeeAsync(employee);


            return employee.ToEmployeeResponse();
        }

        public async Task<bool> DeleteAsync(int id)
        {

            return await _employeesRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<EmployeeResponse>> GetAllAsync()
        {

            var employees =
            await _employeesRepository.GetAllEmployeesAsync();

             
            return employees.Select(em => em.ToEmployeeResponse());
        }

        public async Task<EmployeeResponse?> GetByIdAsync(int id)
        {
            var employee =await _employeesRepository.GetByIdAsync(id);

            if (employee == null)
                return null;

             
            return employee?.ToEmployeeResponse();
        }

        public async Task<EmployeeResponse?> GetByUserIdAsync(Guid userId)
        {
            var employee =await _employeesRepository.GetByUserIdAsync(userId);
             
            if(employee == null)
            {
                return null;
            }

            return employee?.ToEmployeeResponse();
        }

        public async Task<bool> UpdateAsync(EmployeeUpdateRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException();
            }
            var existing = await _employeesRepository.GetByIdAsync(request.EmployeeID);

            if (existing == null) return false;


             
            if (!string.IsNullOrWhiteSpace(request.PersonName))
                existing.PersonName = request.PersonName;

            if (request.HireDate.HasValue)
                existing.HireDate = request.HireDate.Value;

            if (!string.IsNullOrWhiteSpace(request.NumberOfCertificate))
                existing.NumberOfCertificate = request.NumberOfCertificate;

            if (!string.IsNullOrWhiteSpace(request.Phone))
                existing.User.PhoneNumber = request.Phone;

            if (!string.IsNullOrWhiteSpace(request.Email))
                existing.User.Email = request.Email;

            if (!string.IsNullOrWhiteSpace(request.Role))
                existing.Role = request.Role;

            if (request.Salary.HasValue)
                existing.Salary = request.Salary.Value;


            existing.UserId = existing.UserId;
            
             
            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                var hasher = new PasswordHasher<ApplicationUser>();
                existing.User.PasswordHash = hasher.HashPassword(existing.User, request.Password);
            }
             
             
            return await _employeesRepository.UpdateEmployeeAsync(existing);
         }
    }
}
