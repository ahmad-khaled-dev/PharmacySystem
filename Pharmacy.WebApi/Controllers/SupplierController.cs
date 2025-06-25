using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Core.DTO;
using Pharmacy.Core.IServiceContracts;

namespace Pharmacy.WebApi.Controllers
{
    [AllowAnonymous]
    public class SupplierController : CustomController
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var suppliers = await _supplierService.GetAll();
            var result = suppliers.Select(s => s.ToSupplierResponse());
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var supplier = await _supplierService.GetById(id);
            if (supplier is null) return NotFound();
            return Ok(supplier.ToSupplierResponse());
        }

        [HttpPost]
        public async Task<IActionResult> Add(SupplierAddRequest request)
        {
            var added = await _supplierService.Add(request.ToSupplier());
            if (added is null) return BadRequest("Failed to add supplier.");

            return CreatedAtAction(nameof(GetById), new { id = added.SupplierID }, added.ToSupplierResponse());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(SupplierUpdateRequest request ,int id)
        {
            if (id != request.SupplierId) return BadRequest("Does not match ID");

            var updated = await _supplierService.Update(request.ToSupplier());
            return updated ? Ok() : NotFound();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _supplierService.Delete(id);
            return deleted ? Ok() : NotFound();
        }
    }

}
