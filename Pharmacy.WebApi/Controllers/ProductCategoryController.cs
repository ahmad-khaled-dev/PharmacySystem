using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Core.DTO.ProductCategoryDTO;
using Pharmacy.Core.Enum;
using Pharmacy.Core.IServiceContracts;
using Pharmacy.WebApi.Controllers;

namespace Pharmacy.WebAPI.Controllers
{
    [AllowAnonymous]
    public class ProductCategoryController : CustomController
    {
        private readonly ICategoryProductService _categoryService;

        public ProductCategoryController(ICategoryProductService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();


            if (isCustomer())
            {
                var result1 = categories.Select(ca =>ca.ToCategoryResponseCustomer());

                return Ok(result1);
            }

            var result2 = categories.Select(Ca => Ca.ToCategoryResponsePharmacist());

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
        public async Task<IActionResult> Add([FromForm] ProductCategoryAddRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdCategory = await _categoryService.AddCategoryAsync(request.ToCategoryProduct(), request.Image);


            return CreatedAtAction(nameof(GetById), new { id = createdCategory.CategoryId }, createdCategory);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromForm] ProductCategoryUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != request.CategoryID)
                return BadRequest("ID mismatch.");

            var updated = await _categoryService.UpdateCategoryAsync(request.ToCategoryProduct());

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
