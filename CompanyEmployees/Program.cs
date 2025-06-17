using CompanyEmployees;
using CompanyEmployees.Extensions;
using Contract.Interfaces;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using NLog;
using Service.DataShaping;
using Shared.DataTransferObjects;

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
// DataShaper Class Registration
builder.Services.AddScoped<IDataShaper<EmployeeDto>, DataShaper<EmployeeDto>>();
// Filter Attribute Configuration 
builder.Services.ConfigureFilterAttribute();
// SqlContext Configuartion
builder.Services.ConfigureSqlContext(builder.Configuration);
// Register AutoMapper Package
builder.Services.AddAutoMapper(typeof(Program));
// Register Global Exceptional Handling Service
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
// Register Response Caching
builder.Services.AddResponseCaching();
// Enable our custom responses for the API's from the Actions
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
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
    config.InputFormatters.Insert(0, InputOptionFormatters.GetJsonPatchInputFormatter());
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

// CORS configuration added to the application’s pipeline
app.UseCors("CorsPolicy");
// Response Caching Middleware 
app.UseResponseCaching();

app.UseAuthorization();

app.MapControllers();

app.Run();