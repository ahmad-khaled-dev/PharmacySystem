using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Core.DTO.SaleDTO;
using Pharmacy.WebApi.Controllers;


[Authorize]
public class SaleController : CustomController
{
    private readonly ISaleService _saleService;

    public SaleController(ISaleService saleService)
    {
        _saleService = saleService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SaleResponse>>> GetAll()
    {
          
        var result = await _saleService.GetAllSalesAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SaleResponse>> GetById(int id)
    {
        var result = await _saleService.GetSaleByIdAsync(id);
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<SaleResponse>> Create(SaleAddRequest request)
    { 

        var result = await _saleService.AddSaleAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, SaleUpdateRequest request)
    {
        if (id != request.Id)
            return BadRequest();

        var updated = await _saleService.UpdateSaleAsync(request);
        if (!updated)
            return NotFound();

        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _saleService.DeleteSaleAsync(id);
        if (!deleted)
            return NotFound();

        return Ok(deleted);
    }
}
