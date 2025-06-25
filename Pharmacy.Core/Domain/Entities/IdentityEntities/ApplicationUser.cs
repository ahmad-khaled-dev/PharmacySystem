using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Core.Domain.Entities.IdentityEntities
{
   public class ApplicationUser :IdentityUser<Guid>
    {

        public string? RefreshToken { set; get; }
        
        public DateTime RefershTokenExpiration { set; get; }
    }
}
