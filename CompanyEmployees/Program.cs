using CompanyEmployees;
using CompanyEmployees.Extensions;
using Contract.Interfaces;
using Microsoft.AspNetCore.Hosting.Server;
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
// Repository Manager Configuration
builder.Services.ConfigureRepositoryManager();
// Service Manager Configuration (Service Layer)
builder.Services.ConfigureServiceManager();
// SqlContext Configuartion
builder.Services.ConfigureSqlContext(builder.Configuration);
// Register AutoMapper Package
builder.Services.AddAutoMapper(typeof(Program));
// Register Global Exceptional Handling Service
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
#region Add Controller From CompanyEmployees.Prenstation (Presentation Layer)
/// Without this code, our API wouldn’t work, and wouldn’t know where to 
/// route incoming requests. But now, our app will find all of the controllers
/// inside of the Presentation project and configure them with the
/// framework. They are going to be treated the same as if they were defined conventionally.
builder.Services.AddControllers(config => {
    // header:Accept
    config.RespectBrowserAcceptHeader = true;
    //if the client tries to negotiate for the media type the
    //server doesn’t support, it should return the 406 Not Acceptable status
    //code
    config.ReturnHttpNotAcceptable = true;
}).AddXmlDataContractSerializerFormatters().AddApplicationPart(typeof(CompanyEmployees.Presentation.AssemblyReference).Assembly);
#endregion

var app = builder.Build();
// Get Instance of ILoggerManger Service
//var logger = app.Services.GetRequiredService<ILoggerManager>();
// Handling Errors Globally with the Build-In Middleware
//app.ConfigureExceptionHandler(logger);
app.UseExceptionHandler(appError => { });
// Configure the HTTP request pipeline.
if (app.Environment.IsProduction())
{
    //Enables strict transport security headers
    app.UseHsts();
}
//else
//{
//    app.UseDeveloperExceptionPage();
//}

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