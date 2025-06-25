using Microsoft.EntityFrameworkCore;
using Pharmacy.Core.Domain.Entities;
using Pharmacy.Infrastructure.DbContext;

public class MedicineTypeRepository : IMedicineTypeRepository
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<MedicineType> _medicineTypes;

    public MedicineTypeRepository(ApplicationDbContext context)
    {
        _context = context;
        _medicineTypes = context.MedicineTypes;
    }

    public async Task<IEnumerable<MedicineType>> GetAllAsync()
    {

        return await _medicineTypes.ToListAsync();
    }

    public async Task<MedicineType?> GetByIdAsync(int id)
    {

        return await _medicineTypes.Include(c => c.Medicines)
                     .ThenInclude(p => p.Product)
                        .Include(c => c.Medicines).
                        ThenInclude(m => m.Category)
            .FirstOrDefaultAsync(mt => mt.Id == id);
    }

    public async Task<MedicineType> AddAsync(MedicineType medicineType)
    {
        _medicineTypes.Add(medicineType);

        await _context.SaveChangesAsync();
        return medicineType;
    }

    public async Task<bool> UpdateAsync(MedicineType medicineType)
    {
        var existing = await _medicineTypes.FindAsync(medicineType.Id);
        if (existing == null) return false;


        _medicineTypes.Entry(existing).CurrentValues.SetValues(medicineType);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _medicineTypes.FindAsync(id);
        if (entity == null) return false;

        _medicineTypes.Remove(entity);
        await _context.SaveChangesAsync();


        return true;
    }
}
