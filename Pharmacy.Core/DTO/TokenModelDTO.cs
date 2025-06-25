using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Core.DTO
{
   public class TokenModelDTO
    {
        public string? Token { set; get; }

        public string? RefreshToken { set; get; }

    }
}
