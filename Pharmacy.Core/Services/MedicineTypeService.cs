using Pharmacy.Core.Domain.Entities;

public class MedicineTypeService : IMedicineTypeService
{
    private readonly IMedicineTypeRepository _medicinerepository;


    public MedicineTypeService(IMedicineTypeRepository repository)
    {
        _medicinerepository = repository;
    }

    public async Task<IEnumerable<MedicineType>> GetAllAsync()
    {
        var all = await _medicinerepository.GetAllAsync();

        return  all;
    }

    public async Task<MedicineType?> GetByIdAsync(int id)
    {
        var entity = await _medicinerepository.GetByIdAsync(id);

        if (entity is null)
            return null;

        return entity;
    }



    public async Task<MedicineType> AddAsync(MedicineType request)
    {
        if (request is null)
            return null;
         
        var added = await _medicinerepository.AddAsync(request);

        if (added is null)
            return null;

        return added;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var medicineType =await _medicinerepository.GetByIdAsync(id);

        if (medicineType?.Medicines?.Count > 0) return false;

        return await _medicinerepository.DeleteAsync(id);
    }

    
    public async Task<bool> UpdateAsync(MedicineType request)
    {
        var existing = await _medicinerepository.GetByIdAsync(request.Id);

        if (existing == null) return false;

        if(!string.IsNullOrWhiteSpace(request.Name))
        existing.Name = request.Name ?? existing.Name;
          
        return await _medicinerepository.UpdateAsync(existing);
    }
}
