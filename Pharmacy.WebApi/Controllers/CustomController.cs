using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Core.Enum;

namespace Pharmacy.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomController : ControllerBase
    {
        protected bool isCustomer()
        {
            if (User.IsInRole(enEmployeeRole.Customer.ToString()))
            {
                return true;
            }

            return false;
        }
    }
}
