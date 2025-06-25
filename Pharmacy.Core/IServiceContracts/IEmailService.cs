using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Core.IServiceContracts
{
    public interface IEmailService
    {

        /// <summary>
        /// Use To Send Email
        /// </summary>
        /// <param name="toEmail"></param>
        /// <param name="subject"></param>
        /// <param name="htmlContent"></param>
        /// <returns></returns>
         
            Task SendEmailAsync(string toEmail, string subject, string body);
         

    }

}
