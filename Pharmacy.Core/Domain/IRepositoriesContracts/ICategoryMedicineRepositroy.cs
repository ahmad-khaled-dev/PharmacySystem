using Pharmacy.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Core.Domain.IRepositoriesContracts
{
    public interface ICategoryMedicineRepositroy
    {
         
            Task<IEnumerable<MedicineCategory>> GetAllCategoriesAsync(string SeachQuery);

            Task<MedicineCategory?> AddCategoryAsync(MedicineCategory category);

            Task<bool> DeleteCategoryAsync(int categoryId);

            Task<MedicineCategory> FindCategoryByIdAsync(int categoryId);

            Task<bool> UpdateCategoryAsync(MedicineCategory category);
         
        }
}
