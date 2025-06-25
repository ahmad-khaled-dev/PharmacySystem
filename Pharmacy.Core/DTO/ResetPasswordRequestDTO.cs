using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Core.DTO
{
    public class ResetPasswordRequestDTO
    { 
            [Required, EmailAddress]
            public string? Email { get; set; }

            [Required]
            public string ?Token { get; set; }

            [Required, MinLength(6)]
            public string ?NewPassword { get; set; }

            [Required, Compare(nameof(NewPassword))]
            public string? ConfirmPassword { get; set; }
         

    }


}
