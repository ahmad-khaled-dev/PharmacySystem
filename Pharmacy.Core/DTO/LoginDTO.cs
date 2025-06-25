using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Core.DTO
{
  public class LoginDTO
    {

     /*   [Required(ErrorMessage =$"{nameof(UserName)} is  required")]
        [StringLength(maximumLength:20 ,MinimumLength =3,ErrorMessage ="UserName should more than 3 characters")]*/
        public string ?UserName { set; get; }
        
     /*   [Required (ErrorMessage = $"{nameof(Password)} is required")]
        [StringLength(maximumLength:30,MinimumLength =8,ErrorMessage = "password must be at least 6 characters long")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "Password must contain at least one uppercase letter and one number")]*/
        public string ?Password { set; get; }


    }
}
