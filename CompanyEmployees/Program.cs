using CompanyEmployees.Extensions;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// CORSS Origin Resource Sharing Configuration
builder.Services.ConfigureCors();
// IIS Integration Configuration
builder.Services.ConfigureIISIntegration();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    //Enables strict transport security headers
    app.UseHsts();
}

app.UseHttpsRedirection();
//Enables using static files for the request.
app.UseStaticFiles();

//CORS configuration added to the application’s pipeline
app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();