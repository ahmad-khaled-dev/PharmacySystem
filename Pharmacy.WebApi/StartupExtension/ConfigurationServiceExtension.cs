using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Pharmacy.Core.ConfiurationSettings;
using Pharmacy.Core.Domain.Entities.IdentityEntities;
using Pharmacy.Core.Domain.IRepositoriesContracts;
using Pharmacy.Core.IServiceContracts;
using Pharmacy.Core.IServiceContracts.IValidatorContract;
using Pharmacy.Core.Services;
using Pharmacy.Core.Services.ValidatorServices;
using Pharmacy.Infrastructure.Data.Seeders;
using Pharmacy.Infrastructure.DbContext;
using Pharmacy.Infrastructure.Repositories;
using System.Text;
using System.Text.Json.Serialization;
namespace Pharmacy.WebApi.StartupExtension
{
    public static class ConfigurationServiceExtension
    {

        public static IServiceCollection configureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Addd auto controller to service
           services.AddControllers()
     .AddJsonOptions(options =>
     {
         options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
     });


            //Add authorization policy

            services.AddAuthorization(options =>
            {
                var policyAuthentication = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.FallbackPolicy = policyAuthentication;

            });


            //Add ApplicationDbContext to service

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("Default"),
                      sqlOptions => sqlOptions.EnableRetryOnFailure()
                      );
                
            });


            //Add repository ,service for Identity

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredUniqueChars = 1;


                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                 
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders()
            .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>()
            .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>()
             ;


            //Add Service Token
            services.AddTransient<IJwtService, JwtService>();



            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
                .AddJwtBearer(options =>
                 {
                      
                     options.TokenValidationParameters = new TokenValidationParameters()
                     {

                         ValidateIssuer = true,
                         ValidateAudience = true,
                         ValidateLifetime = true,
                         ValidateIssuerSigningKey = true,


                         ValidIssuer = configuration["Jwt:Issuer"],
                         ValidAudience = configuration["jwt:Audience"],
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]! )),


                     };


                 }
            );


             

            //Add Swagger
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "api.Xml"));
            });

            //Config JwtSettings

            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));


            //Add Email Settings to IOC
    //       services.Configure<EmailSettings>(
    //configuration.GetSection("EmailSettings"));

    //        services.AddScoped<IEmailService, EmailService>();



            //Employee

            services.AddTransient<IEmployeesRepository, EmployeesRepositoy>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IUserRegistrationService, UserRegistrationService>();

  
            //Purchase
             
            services.AddTransient<IPurchaseRepository, PurchasesRepository>();
            services.AddTransient<IPurchaseService, PurchaseService>();


            //UnitOfWork (Transaction)

            services.AddTransient<IUnitOfWorkService, UnitOfWorkService>();


            //Get Current User 

            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddHttpContextAccessor();


            // ImageFile Service
            services.AddScoped<IFileStorageService, FileStorageService>();

            //Sale
             
            services.AddTransient<ISaleRepository, SaleRepository>();
            services.AddTransient<ISaleService, SaleService>();


            //Product 

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductSevice, ProductService>();

            //Category Product

            services.AddTransient<ICategoryProductsRepositroy, CategoryProductsRepository>();
            services.AddTransient<ICategoryProductService, CategoryProductService>();

            //MedicineCategory

            services.AddTransient<ICategoryMedicineService ,CategoryMedicineService>();
            services.AddTransient<ICategoryMedicineRepositroy ,CategoryMedicineRepository>();

            //MedicineType
            services.AddTransient<IMedicineTypeService ,MedicineTypeService>();
            services.AddTransient<IMedicineTypeRepository ,MedicineTypeRepository>();

            //Supplier Repository 
            services.AddTransient<ISupplierRepositroy, SupplierRepository>();
            services.AddTransient<ISupplierService, SupplierService>();


            //Validator Purchase
            services.AddScoped<IPurchaseValidator, PurchaseValidator>();


            //Batch
            services.AddScoped<IBatchRepository, BatcheRepository>();

            //Sale
            services.AddScoped<ISaleRepository, SaleRepository>();
            services.AddScoped<ISaleService, SaleService>();

            //InventoryItem
            services.AddScoped<IInventoryItemRepository, InventoryItemRepository>();


            //Seed Data by bogus library 
             
            services.AddHostedService<DbSeederService>();


            return services;
        }

    }
}