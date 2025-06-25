using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Core.Domain.Entities;
using Pharmacy.Core.DTO;
using Pharmacy.Core.DTO.PurchaseDTO;
using Pharmacy.Core.IServiceContracts;

namespace Pharmacy.WebApi.Controllers
{
    [AllowAnonymous]

    public class PurchaseController : CustomController
    {
        private readonly IPurchaseService _purchaseService ;

        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPurchases()
        {

            var result= await  _purchaseService.GetAllPurchasesAsync();

            var purchases = result.Select(p => p.ToPurchaseResponse());

            return Ok(result);  
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetPurchaseById (int id)
        {

            Purchase purchase= await _purchaseService.GetPurchaseByIdAsync(id);

            if (purchase == null) return NotFound();

            return Ok(purchase.ToPurchaseResponse());   
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePurchase( [FromBody] PurchaseUpdateRequest purchaseUpdate ,int id)
        {
            if(id != purchaseUpdate.PurchaseID)
            {
                return Problem("ProductID does not match !");
            }

            var success = await _purchaseService.UpdatePurchaseAsync(purchaseUpdate.ToPurchase());

            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpPost]

        public async Task <IActionResult> AddPurchase(PurchaseAddRequest purchaseAddRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(purchaseAddRequest == null)
            {
                return BadRequest();
            }

            var result = await _purchaseService.AddPurchaseAsync(purchaseAddRequest.ToPurchase());

            return CreatedAtAction(nameof(GetPurchaseById), new { id = result.PurchaseID }, result.ToPurchaseResponse());
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeletePurchase (int id)
        {

            var deleted = await _purchaseService.DeletePurchaseAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
         
    }
}
