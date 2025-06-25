using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Core.DTO.ProductDTO;
using Pharmacy.Core.IServiceContracts;


namespace Pharmacy.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ProductController : ControllerBase
    {
        private readonly IProductSevice _productSevice;

        public ProductController(IProductSevice productSevice)
        {
            _productSevice = productSevice;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products= await _productSevice.GetAllProductAsync();

            return Ok(products);
        }
        
        [HttpGet("{id:int}")]
        public async Task <IActionResult>  GetById(int id)
        {
           var product= await _productSevice.FindProductByIdAsync(id);

            if (product == null) return NotFound();

            return Ok(product);
        }
        
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id ,[FromForm]ProductUpdateRequest productUpdateRequest)
        {
            if (id != productUpdateRequest.ProductId)
                return BadRequest("Mismatched product ID");

            bool isUpdated= await _productSevice.UpdateProductByIdAsync(productUpdateRequest);

            if (!isUpdated) return NotFound();

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Create( [FromForm]ProductAddRequest productAddRequest)
        {
             if (productAddRequest == null) return BadRequest();

            var product = await _productSevice.AddProductAsync(productAddRequest);

            if (product == null) return Problem("Unable to create product .");

            return CreatedAtAction(nameof(GetById), new { id = product.ProductId}, product);
        }
         

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool deleted = await _productSevice.DeleteProductByIdAsync(id);

            if (!deleted) return NotFound();

            return NoContent();
        }
    }

}
