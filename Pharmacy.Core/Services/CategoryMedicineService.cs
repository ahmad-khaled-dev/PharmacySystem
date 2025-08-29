using Microsoft.AspNetCore.Http;
using Pharmacy.Core.Domain.Entities;
using Pharmacy.Core.Domain.IRepositoriesContracts;
using Pharmacy.Core.DTO.MedicineCategoryDTO;
using Pharmacy.Core.IServiceContracts;

namespace Pharmacy.Core.Services
{
    public class CategoryMedicineService : ICategoryMedicineService
    {
        private readonly ICategoryMedicineRepositroy _categoryMedicineRepositroy;
        private readonly IFileStorageService _fileStorageService;

        public CategoryMedicineService(ICategoryMedicineRepositroy categoryMedicineRepositroy, IFileStorageService fileStorageService)
        {
            _categoryMedicineRepositroy = categoryMedicineRepositroy;
            _fileStorageService = fileStorageService;
        }


        public async Task<IEnumerable<MedicineCategory>> GetAllCategoriesAsync(string SearchQuery)
        {
            var categories = await _categoryMedicineRepositroy.GetAllCategoriesAsync(SearchQuery);

            return categories.Select(cm => new MedicineCategory()
            {
                MedicineCategoryID = cm.MedicineCategoryID,
                Name = cm.Name,
                Image = _fileStorageService.GetFullImageUrl(cm.Image)
               
                
            }).ToList();
        }

        public async Task<MedicineCategory> AddCategoryAsync(MedicineCategory medicineCategory,IFormFile ?Image)
        {
            if (medicineCategory is null)
                return null;


            if (Image != null && Image.Length > 0)
            {
                medicineCategory.Image = await _fileStorageService.SaveFileAsync(Image, "MedicineCategory");

            }


            var categoryMedicine = await _categoryMedicineRepositroy.AddCategoryAsync(medicineCategory);


            if (categoryMedicine is null)
                return null;

            categoryMedicine.Image = _fileStorageService.GetFullImageUrl(categoryMedicine.Image);

            return categoryMedicine;
        }

        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            var MCategory = await _categoryMedicineRepositroy.FindCategoryByIdAsync(categoryId);

            if (MCategory == null) return false;


            if (MCategory?.Medicines?.Count > 0) return false;

            var result = await _categoryMedicineRepositroy.DeleteCategoryAsync(categoryId);

            if (result && !string.IsNullOrWhiteSpace(MCategory.Image))
            {
                _fileStorageService.DeleteFile(MCategory.Image);
            }

            return result;
        }

        public async Task<MedicineCategory?> FindCategoryByIdAsync(int categoryId)
        {
            var category = await _categoryMedicineRepositroy.FindCategoryByIdAsync(categoryId);

            if (category == null)
                return null;


             
            return new MedicineCategory()
            {
                MedicineCategoryID = category.MedicineCategoryID,
                Name = category.Name,
                Image = _fileStorageService.GetFullImageUrl(category.Image),
                Medicines = category.Medicines
            };
        }

        public async Task<bool> UpdateCategoryAsync(MedicineCategory medicineCategory ,IFormFile? Image=null)
        {

            var existingcategory = await _categoryMedicineRepositroy.FindCategoryByIdAsync(medicineCategory.MedicineCategoryID);



            if (existingcategory == null)
            {
                return false;
            }

            string? imagePath = existingcategory.Image;

            if (Image != null && Image.Length > 0)
            {
                _fileStorageService.DeleteFile(existingcategory.Image!); 
               existingcategory.Image  = await _fileStorageService.SaveFileAsync(Image, "MedicineCategory");
            }

            if(!string.IsNullOrWhiteSpace(medicineCategory.Name))
            {
                existingcategory.Name = medicineCategory.Name;
            }
             
          return  await _categoryMedicineRepositroy.UpdateCategoryAsync(existingcategory);
        }
    }
}
