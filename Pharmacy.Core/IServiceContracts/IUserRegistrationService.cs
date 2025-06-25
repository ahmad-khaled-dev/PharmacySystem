using Microsoft.AspNetCore.Identity;
using Pharmacy.Core.Domain.Entities.IdentityEntities;
using Pharmacy.Core.DTO.EmployeeDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Core.IServiceContracts
{
   public interface IUserRegistrationService
    {
        Task<IdentityResult> RegisterUserAsync(EmployeeAddRequest employeeAddRequest);

        Task<(bool Success, IEnumerable<IdentityError> Errors, string Message)> UpdateUserWithEmployeeAsync(EmployeeUpdateRequest request);

        public   Task<bool> ChangeUserRoleAsync(ApplicationUser user, string newRole);
    }
}
