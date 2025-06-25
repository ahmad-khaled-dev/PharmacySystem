using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Core.ConfiurationSettings
{
    public class JwtSettings
    {
        public string? Issuer { set; get; } 

        public string? Audience { set; get; }  

        public double ExiprationMinutes { set; get; }

        public string? Key { set; get; }  

    }


}
