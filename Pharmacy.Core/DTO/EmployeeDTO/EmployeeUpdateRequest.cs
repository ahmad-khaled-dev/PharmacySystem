using Pharmacy.Core.Domain.Entities.IdentityEntities;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Core.DTO.EmployeeDTO
{
    public class EmployeeUpdateRequest
    {
            [Required]
          public int EmployeeID { set; get; }
             
         
           [StringLength(100, MinimumLength = 8, ErrorMessage = "كلمة السر يجب أن تكون أكبر من 8 أحرف")]
             
           public string? Password { set; get; }


            [Required]
             [StringLength(50,MinimumLength = 3,ErrorMessage = " الاسم يجب ان يكون على الاقل ثلاث حروف")]
            public string? PersonName { set; get; }

            [EmailAddress]
               
            public string? Email { set; get; }

            [RegularExpression("^(\\d{1,15})$")]
            public string? Phone { set; get; }

            public DateTime? HireDate { set; get; } = DateTime.Now;


            public float? Salary { set; get; }
         
            public string? NumberOfCertificate { set; get; }

            public string? Role { set; get; }= "User";

            public  Guid ?UserID { set; get; }
  
            public Employee ToEmployee()
            {
            return new Employee()
            {

                EmployeeID = this.EmployeeID,
                Salary = this.Salary,
                HireDate = this.HireDate,
                PersonName = this.PersonName,
                NumberOfCertificate = this.NumberOfCertificate,
                Role = this.Role ,
                UserId=this.UserID,
                Phone=this.Phone
                 
            };
            }
        }


    }




 