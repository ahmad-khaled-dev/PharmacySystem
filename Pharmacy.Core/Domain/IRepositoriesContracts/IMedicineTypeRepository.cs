using Pharmacy.Core.Domain.Entities;

public interface IMedicineTypeRepository
{
    Task<IEnumerable<MedicineType>> GetAllAsync();
    
    Task<MedicineType?> GetByIdAsync(int id);
    
    Task<MedicineType> AddAsync(MedicineType medicineType);
    
    Task<bool> UpdateAsync(MedicineType medicineType);
    
    Task<bool> DeleteAsync(int id);

}
