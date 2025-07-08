using Pharmacy.Infrastructure.Data;
using Pharmacy.WebApi.StartupExtension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.configureServices(builder.Configuration);

// ? Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy
            .WithOrigins("http://localhost:3000") // ? »Ê—  React
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

 
using (var scope = app.Services.CreateScope())
{
   
    var services = scope.ServiceProvider;
    await IdentityInitializer.SeedAdminUserAsync(services);
}

<<<<<<< HEAD
// ? Configure Middleware Order („Â„ Ãœ«)
=======
 
>>>>>>> Add Seed data with library bogusinitial

// Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles();

app.UseRouting();

// ? «” Œœ„ CORS Â‰«
app.UseCors("AllowReactApp");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
