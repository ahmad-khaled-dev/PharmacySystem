using Pharmacy.Core.Domain.Entities.IdentityEntities;
using System.ComponentModel.DataAnnotations;


namespace Pharmacy.Core.DTO.EmployeeDTO
{
   public class EmployeeAddRequest
    {
         
        [StringLength(100, MinimumLength = 8, ErrorMessage = "كلمة السر يجب أن تكون أكبر من 8 أحرف")]
        [Required]
        public string Password { set; get; }

        public string UserName {  set; get; }
         

        [Required]
        public string  PersonName { set; get; }

        [EmailAddress]
        [Required]
        public string Email { set; get; }

        [RegularExpression("^(\\d{1,15})$")]
        public string? Phone { set; get; }

        public DateTime? HireDate { set; get; } = DateTime.Now;
        
        
        public float? Salary{ set; get; }

        [Required]
        public string NumberOfCertificate { set; get; }

        public string Role{ set; get; } = "User";
        
         
        public Guid? UesrId { set; get; }

         

        public Employee ToEmployee()
        {
            return new Employee()
            {
                Salary = this.Salary,
                HireDate = this.HireDate,
                PersonName = this.PersonName,
                NumberOfCertificate = this.NumberOfCertificate,
                Role = this.Role ,
               UserId = this.UesrId,
               Phone=this.Phone,
                 

            };
        }
    }
}
