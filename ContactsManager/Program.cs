using ServiceContracts;
using Services;
using Microsoft.EntityFrameworkCore;
using Entities;
using RepositoryContracts;
using Repositories;
using Serilog;
using Serilog.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

//builder.Host.ConfigureLogging(logginProvider =>
//{
//    logginProvider.ClearProviders();
//    logginProvider.AddConsole();
//    logginProvider.AddDebug();
//    logginProvider.AddEventLog();
//});

//Serilog
builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, 
    LoggerConfiguration loggerConfiguration) =>
{
    loggerConfiguration
        //read configuration settings from built-in IConfiguration
        .ReadFrom.Configuration(context.Configuration)
        //read out current app's services and make them available to serilog
        .ReadFrom.Services(services);
    
});

builder.Services.AddControllersWithViews();

//add services into IoC container

builder.Services.AddScoped<ICountriesRepository, CountriesRepository>();
builder.Services.AddScoped<IPersonsRepository, PersonsRepository>();

builder.Services.AddScoped<ICountriesService, CountriesService>();
builder.Services.AddScoped<IPersonsService, PersonsService>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields =
        Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestProperties |
        Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponsePropertiesAndHeaders;
});

var app = builder.Build();

app.UseSerilogRequestLogging();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpLogging();

//app.Logger.LogDebug("debug-message");
//app.Logger.LogInformation("debug-message");
//app.Logger.LogWarning("debug-message");
//app.Logger.LogError("debug-message");
//app.Logger.LogCritical("debug-message");

if (builder.Environment.IsEnvironment("Test") == false)
    Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", wkhtmltopdfRelativePath: "Rotativa");

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();

public partial class Program { } // make the auto-generated Program accessible programmaticaly