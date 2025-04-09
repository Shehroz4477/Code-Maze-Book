using CompanyEmployees.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
using NLog;

var builder = WebApplication.CreateBuilder(args);

//Load Config file for logger
LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "./nlog.config"));

// Add services to the container.

// CORSS Origin Resource Sharing Configuration
builder.Services.ConfigureCors();
// IIS Integration Configuration
builder.Services.ConfigureIISIntegration();
// Logger Service Configuration
builder.Services.ConfigureLoggerService();
// Repository Manager Confiduration
builder.Services.ConfigureRepositoryManager();

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
//Forward proxy headers to the current request.
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

//CORS configuration added to the application’s pipeline
app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();