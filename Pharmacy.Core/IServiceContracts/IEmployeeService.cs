using Pharmacy.Core.DTO.EmployeeDTO;



namespace Pharmacy.Core.IServiceContracts
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeResponse>> GetAllAsync();

        Task<EmployeeResponse?> GetByIdAsync(int id);
        
        Task<EmployeeResponse?> GetByUserIdAsync(Guid userId);
        
        Task<EmployeeResponse> CreateAsync(EmployeeAddRequest request);
        
        Task<bool> UpdateAsync(EmployeeUpdateRequest request);
        
        Task<bool> DeleteAsync(int id);
    }
}
