using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pharmacy.Core.Domain.Entities.IdentityEntities;
using Pharmacy.Infrastructure.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Infrastructure.Data
{
  public  static class IdentityInitializer
    {
        public static async Task SeedAdminUserAsync(IServiceProvider provider)
        {
            var userManager = provider.GetRequiredService<UserManager<ApplicationUser>>();

            var roleManager = provider.GetRequiredService<RoleManager<ApplicationRole>>();


            var adminRole = "Admin";
             
            if(! await roleManager.RoleExistsAsync(adminRole))
            {

                await roleManager.CreateAsync(new ApplicationRole()
                {
                    Name=adminRole
                });


            }

            var defaultEmail = "toxerov558@neuraxo.com";

            ApplicationUser defaultUser = await userManager.FindByEmailAsync(defaultEmail);

            if(defaultUser == null)
            {
                var user = new ApplicationUser()
                {
                    UserName="Admin",
                    Email= "toxerov558@neuraxo.com",
                     
                };

                IdentityResult result = await userManager.CreateAsync(user, "Admin@123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, adminRole);
                }
            }


        }
    }
}
