using Microsoft.AspNetCore.Http;
using Pharmacy.Core.Domain.Entities;
using Pharmacy.Core.DTO.MedicineCategoryDTO;
using Pharmacy.Core.DTO.ProductCategoryDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Core.IServiceContracts
{
   public interface ICategoryMedicineService
    {
        Task<IEnumerable<MedicineCategory>> GetAllCategoriesAsync(string SeachQuery);

        Task<MedicineCategory> AddCategoryAsync(MedicineCategory  medicineCategory, IFormFile? Image = null);

        Task<bool> DeleteCategoryAsync(int categoryId);

        Task<MedicineCategory?> FindCategoryByIdAsync(int categoryId);

        Task<bool> UpdateCategoryAsync(MedicineCategory medicineCategory, IFormFile? Image = null);

        //Task<IEnumerable<MedicineCategory>> SearchCategoriesByNameAsync(string name);
    }
}
