using Pharmacy.Core.Domain.Entities.IdentityEntities;
using Pharmacy.Core.DTO.EmployeeDTO;


namespace Pharmacy.Core.DTO.EmployeeDTO
{
    public class EmployeeResponse
    {

        public int EmployeeID { set; get; }

         
        public string UserName { set; get; }

          public string PersonName { set; get; }

         public string Email { set; get; }

         public string? Phone { set; get; }

        public DateTime? HireDate { set; get; } = DateTime.Now;

         public float? Salary { set; get; }

         public string NumberOfCertificate { set; get; }

        public string? Role { set; get; }
         


    }
}



 

public static class EmployeeExtension
        {
    public static EmployeeResponse ToEmployeeResponse(this Employee employee)
    {
        if (employee == null)
            throw new ArgumentNullException(nameof(employee));
         
        return new EmployeeResponse
        {
            EmployeeID = employee.EmployeeID,
            UserName = employee.User?.UserName,
            PersonName = employee.PersonName ,
            Email = employee.User?.Email ,
            Phone = employee.User?.PhoneNumber ,
            HireDate = employee.HireDate,
            Salary = employee.Salary,
            NumberOfCertificate = employee.NumberOfCertificate,
            Role = employee.Role
            

        };
    }


}


