using Pharmacy.Core.Domain.Entities.IdentityEntities;
using Pharmacy.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Core.IServiceContracts
{
   public interface IJwtService
    {
       public  Task < AuthenticationResponse >createJwtToken(ApplicationUser user);

       public ClaimsPrincipal? GetPrincipleFromJwtToken(string? token);

    }
}
