using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Core.Domain.Entities.IdentityEntities;
using Pharmacy.Core.DTO.EmployeeDTO;
using Pharmacy.Core.IServiceContracts;

namespace Pharmacy.Core.Services
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly IEmployeeService _employeeService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IUnitOfWorkService _unitOfWorkService;

        public UserRegistrationService(IEmployeeService employeeService, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IUnitOfWorkService unitOfWorkService)
        {
            _employeeService = employeeService;
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWorkService = unitOfWorkService;
        }

        public async Task<IdentityResult> RegisterUserAsync(EmployeeAddRequest employeeAddRequest)
        {
            var strategy = _unitOfWorkService.CreateExecutionStrategy();

            IdentityResult result = IdentityResult.Failed();

            await strategy.ExecuteAsync(async () =>
            {
                await _unitOfWorkService.BeginTransactionAsync();

                try
                {
                    var user = new ApplicationUser
                    {
                        Email = employeeAddRequest.Email,
                        PhoneNumber = employeeAddRequest.Phone,
                        UserName = employeeAddRequest.Email.Split('@')[0] ?? employeeAddRequest.Email
                    };

                    if (await _roleManager.RoleExistsAsync("User") == false && employeeAddRequest.Role == "User")
                    {
                        var role = new ApplicationRole { Name = "User" };
                        await _roleManager.CreateAsync(role);
                    }

                    result = await _userManager.CreateAsync(user, employeeAddRequest.Password);

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, employeeAddRequest.Role);
                        employeeAddRequest.UesrId = user.Id;  
                    }

                    await _unitOfWorkService.SaveChangesAsync();
                    await _unitOfWorkService.CommitTransactionAsync();
                }
                catch
                {
                    await _unitOfWorkService.RollbackTransactionAsync();
                    throw;
                }
            });

            return result;
        }

        public async Task<(bool Success, IEnumerable<IdentityError> Errors, string Message)> UpdateUserWithEmployeeAsync(EmployeeUpdateRequest request)
        {
            var employee = await _employeeService.GetByIdAsync(request.EmployeeID);

            if (employee == null)
            {
                return (false, null, "الموظف غير موجود");
            }

            var user = await _userManager.FindByEmailAsync(employee.Email);
            if (user == null )

                return (false, null, "المستخدم غير موجود")!;

             
            await _unitOfWorkService.BeginTransactionAsync();

            try
            {
                if(!string.IsNullOrWhiteSpace(request.Email))
                user.UserName = request.Email.Split('@')[0] ?? employee.Email;
                user.Email = request.Email ?? employee.Email;
                user.PhoneNumber = request.Phone ?? employee.Phone;
                 

                var updateUserResult = await _userManager.UpdateAsync(user); 

                if (!updateUserResult.Succeeded)
                    return (false, updateUserResult.Errors, "فشل تحديث المستخدم");



                if (!string.IsNullOrWhiteSpace(request.Role))
                {
                    var result = await ChangeUserRoleAsync(user, request.Role);

                    if (!result)
                        return (false,null,"فشل تجديث ال role");

                }

                var success = await _employeeService.UpdateAsync(request);

                if (!success)
                    return (false, null, "فشل تحديث بيانات الموظف")!;

                await _unitOfWorkService.CommitTransactionAsync();
                return (true, null, "تم التحديث بنجاح")!;
            }
            catch (Exception e)
            {
                await _unitOfWorkService.RollbackTransactionAsync();
                throw;
            }
        }
         
        public async Task<bool> ChangeUserRoleAsync(ApplicationUser user, string newRole)
        {
            
            if (user == null)
                return false;

             
            var currentRoles = await _userManager.GetRolesAsync(user);

             
            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeResult.Succeeded)
                return false;

            
            var roleExists = await _roleManager.RoleExistsAsync(newRole);

            if (!roleExists)
            { 

                await _roleManager.CreateAsync(new ApplicationRole() { Name=newRole});
            }

 
            var addResult = await _userManager.AddToRoleAsync(user, newRole);
            return addResult.Succeeded;
        }

    }

} 
