using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Core.DTO.MedicineCategoryDTO;
using Pharmacy.Core.IServiceContracts;
using Pharmacy.WebApi.Controllers;

namespace Pharmacy.WebAPI.Controllers
{ 


    [AllowAnonymous]
    public class MedicineCategoryController : CustomController
    {
        private readonly ICategoryMedicineService _categoryService;

        public MedicineCategoryController(ICategoryMedicineService categoryService)
        {
            _categoryService = categoryService;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();

            if (isCustomer())
            {
            var result1 =categories.Select(c => c.ToCategoryResponseCustomer());

                return Ok(result1);
            }

            var result2= categories.Select(c => c.ToCategoryResponsePharmacist());

            return Ok(result2); 
        }

         
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.FindCategoryByIdAsync(id);
            if (category == null)
                return NotFound();

            if (isCustomer())
            {
                var result1 = category.ToCategoryResponseCustomer();
            return Ok(result1);
            }

            var result2 = category.ToCategoryResponsePharmacist();

            return Ok(result2);
        }

         
        [HttpPost]
        public async Task<IActionResult> AddCategoryProduct([FromForm] MedicineCategoryAddRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdCategory = await _categoryService.AddCategoryAsync(request.ToCategoryMedicine(),request.Image);

            if (isCustomer())
            {
                var result1 = createdCategory.ToCategoryResponseCustomer();

                return CreatedAtAction(nameof(GetById), new { id = result1.CateogryID }, result1);
            }

            var result2 = createdCategory.ToCategoryResponsePharmacist();

            return CreatedAtAction(nameof(GetById), new { id = result2.CateogryID }, result2);

        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromForm] MedicineCategoryUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != request.CategoryID)
                return BadRequest("ID mismatch.");

            var updated = await _categoryService.UpdateCategoryAsync(request.ToCategoryMedicine());

            if (!updated)
                return NotFound();

            return NoContent();
        }

         
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategoryProduct(int id)
        {
            var deleted = await _categoryService.DeleteCategoryAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
