/*using Microsoft.AspNetCore.Authorization;
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
    public class ManageUserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IEmployeeService _employeeService;

        public ManageUserController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IEmployeeService employeeService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _employeeService = employeeService;
        }

        [HttpGet]
        public  async Task<IEnumerable<EmployeeResponse>> GetAllEmployee()
        {

            return  await _employeeService.GetAllAsync();

       //     employees.Select(em => em.Role=_userManager.GetRolesAsync());
        }

        [HttpGet("by-id/{id:int}")]
        public async Task<IActionResult> GetUserById(int id)
        {
         
            var employee =await _employeeService.GetByIdAsync(id);

            return employee is null ? NotFound() : Ok(employee);
        }

        [HttpGet("by-user-id/{userId:guid}")]

        public async Task<IActionResult> GetUserByUserId(Guid userId)
        {
            
            var employee =await _employeeService.GetByUserIdAsync(userId);

            return employee is null ? NotFound() : Ok(employee);
        }


        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody]  EmployeeAddRequest employeeAddRequest)
        {
            if (employeeAddRequest == null)
            {
                return Problem();
            }
            ApplicationUser user = new ApplicationUser()
            {
                Email = employeeAddRequest.Email,
                PhoneNumber = employeeAddRequest.phone,
                UserName = employeeAddRequest.UserName
            };


            if (await _roleManager.RoleExistsAsync("User") == false)
            {

                ApplicationRole role = new ApplicationRole()
                {
                    Name = "User"
                };

              await  _roleManager.CreateAsync(role);
            }

            var result = await _userManager.CreateAsync(user, employeeAddRequest.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await _userManager.AddToRoleAsync(user, "User");

            employeeAddRequest.User = user;
            employeeAddRequest.UesrId = user.Id;   

             
          await  _employeeService.CreateAsync(employeeAddRequest);


            return NoContent();
        }
         
        [HttpPut]
      
        public async Task<IActionResult> UpdateUser([FromBody] EmployeeUpdateRequest request)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


                 var employeeResponse = await _employeeService.GetByIdAsync(request.EmployeeID);

            ApplicationUser user=await 
            _userManager.FindByEmailAsync(employeeResponse.Email);

            if (employeeResponse == null || user == null)
                return NotFound("الموظف أو المستخدم غير موجود");

            user.UserName = request.UserName??employeeResponse.UserName;
            user.Email = request.Email?? employeeResponse.Email;
            user.PhoneNumber = request.Phone ?? employeeResponse.Phone;
             
            var updateUserResult = await _userManager.UpdateAsync(user);

            if (!updateUserResult.Succeeded)
            {
                return BadRequest(updateUserResult.Errors);
            }

             
            var success = await _employeeService.UpdateAsync(request);

            if (!success)
                return NotFound("فشل تحديث بيانات الموظف");

            return Ok("تم تحديث المستخدم بنجاح");
        }
        

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var success = await _employeeService.DeleteAsync(id);
            return success ? Ok() : NotFound();
        }
    }
}
*/