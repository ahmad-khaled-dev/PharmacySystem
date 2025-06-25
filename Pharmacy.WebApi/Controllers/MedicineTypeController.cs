using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class MedicineTypeController : ControllerBase
{
    private readonly IMedicineTypeService _service;

    public MedicineTypeController(IMedicineTypeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();


        var result2= result.Select(mt => mt.ToMedicineTypeResponse());

        return Ok(result2);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);

        if (result == null) return NotFound();
        
        return Ok(result.ToMedicineTypeDetailResponse());
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] MedicineTypeAddRequest request)
    {
        var result = await _service.AddAsync(request.ToMedicineType());

       var result2 = result.ToMedicineTypeResponse();
        return CreatedAtAction(nameof(GetById), new { id = result2.Id }, result2);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] MedicineTypeUpdateRequest request)
    {
        if (id != request.Id) return BadRequest("ID mismatch");

        var result = await _service.UpdateAsync(request.ToMedicineType());
        
        return result ? Ok() : NotFound();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);

        return result ? Ok() : NotFound();
    }
}
