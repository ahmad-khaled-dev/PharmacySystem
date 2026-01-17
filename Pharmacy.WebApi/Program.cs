using Pharmacy.Infrastructure.Data;
using Pharmacy.WebApi.StartupExtension;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
            ;
    });
});

// Add services to the container.
builder.Services.configureServices(builder.Configuration);

// ? Add CORS
////builder.Services.AddCors(options =>
////{
////    options.AddPolicy("AllowReactApp", policy =>
////    {
////        policy
////            .WithOrigins("http://0.0.0.0:3000") // 
////            .AllowAnyHeader()
////            .AllowAnyMethod();
////    });
////});
///




var app = builder.Build();

 
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    await IdentityInitializer.SeedAdminUserAsync(services);
    await CategorySeeder.SeedCategoryAsync(services);
}

// Configure the HTTP request pipeline.


//Use Swagger


app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles();

app.UseRouting();


app.Use(async (context, next) =>
{
    Console.WriteLine("Request path: " + context.Request.Path);
    await next.Invoke();
});
app.UseCors("AllowAll");


app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
