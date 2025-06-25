using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Core.Domain.Entities.IdentityEntities;
using Pharmacy.Core.DTO.EmployeeDTO;
using Pharmacy.Core.IServiceContracts;

namespace Pharmacy.WebApi.Controllers
{

    /// <summary>
    /// //This Controller use by Admin to Manpulate Users
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class EmployeeController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IEmployeeService _employeeService;
        private readonly IUserRegistrationService _userRegistrationService;

        public EmployeeController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IEmployeeService employeeService, IUserRegistrationService userRegistrationService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _employeeService = employeeService;
            _userRegistrationService = userRegistrationService;
        }

        [HttpGet]
        public async Task<IEnumerable<EmployeeResponse>> GetAllEmployees()
        {

            return await _employeeService.GetAllAsync();
        }

        [HttpGet("by-id/{id:int}")]
        public async Task<IActionResult> GetUserById(int id)
        {

            var employee = await _employeeService.GetByIdAsync(id);

            return employee is null ? NotFound() : Ok(employee);
        }

        [HttpGet("by-user-id/{userId:guid}")]

        public async Task<IActionResult> GetUserByUserId(Guid userId)
        {

            var employee = await _employeeService.GetByUserIdAsync(userId);

            return employee is null ? NotFound() : Ok(employee);
        }


        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] EmployeeAddRequest employeeAddRequest)
        {
            if (employeeAddRequest == null)
            {
                return Problem();
            }


            var result = await _userRegistrationService.RegisterUserAsync(employeeAddRequest);

            if (!result.Succeeded)
                return BadRequest(result.Errors);


            var resultEmployee = await _employeeService.CreateAsync(employeeAddRequest);




            return CreatedAtAction(
    nameof(GetUserById),
    new { id = resultEmployee.EmployeeID},
    resultEmployee
);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] EmployeeUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != request.EmployeeID)
                return BadRequest("معرف الموظف غير متطابق.");

             

            var (success, errors, message) = await _userRegistrationService.UpdateUserWithEmployeeAsync(request);


            if (!success)
            {
                if (errors != null)
                {
                    return BadRequest(errors);
                }

                return NotFound(message);
            }

            return Ok(message);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var success = await _employeeService.DeleteAsync(id);
            return success ? Ok() : NotFound();
        }
    }
}
