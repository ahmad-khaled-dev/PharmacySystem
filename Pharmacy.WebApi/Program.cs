using Pharmacy.Infrastructure.Data;
using Pharmacy.WebApi.StartupExtension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.configureServices(builder.Configuration);
 
var app = builder.Build();

//Seed data



// «” œ⁄«¡ Seeder œ«Œ· scope
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    await IdentityInitializer.SeedAdminUserAsync(services);
}

// Configure the HTTP request pipeline.


//Use Swagger


app.UseSwagger();

app.UseSwaggerUI();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
