using CompanyEmployees.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Routing;
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
// Repository Manager Configuration
builder.Services.ConfigureRepositoryManager();
// Service Manager Configuration (Service Layer)
builder.Services.ConfigureServiceManager();
// SqlContext Configuartion
builder.Services.ConfigureSqlContext(builder.Configuration);

#region Add Controller From CompanyEmployees.Prenstation (Presentation Layer)
/// Without this code, our API wouldn’t work, and wouldn’t know where to 
/// route incoming requests. But now, our app will find all of the controllers
/// inside of the Presentation project and configure them with the
/// framework. They are going to be treated the same as if they were defined conventionally.
builder.Services.AddControllers().AddApplicationPart(typeof(CompanyEmployees.Presentation.AssemblyReference).Assembly);
#endregion

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