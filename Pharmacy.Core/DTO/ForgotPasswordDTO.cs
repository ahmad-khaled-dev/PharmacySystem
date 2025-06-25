using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Core.DTO
{
  public  class ForgotPasswordDTO
    {
        [Required]
        [EmailAddress]
       public string ? Email { set; get; }

        [Required]
        public string? ClientURl { set; get; }
    }
}
