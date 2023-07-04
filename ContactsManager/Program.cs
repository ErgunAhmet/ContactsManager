using ServiceContracts;
using Services;
using Microsoft.EntityFrameworkCore;
using Entities;
using Microsoft.Data.SqlClient;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

//add services into IOC container
builder.Services.AddScoped<ICountriesService, CountriesService>();
builder.Services.AddScoped<IPersonsService, PersonsService>();

builder.Services.AddDbContext<PersonsDbContext>(
	(options =>
	{
		//options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
		options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
	})	
);
var app = builder.Build();

if (builder.Environment.IsDevelopment())
	app.UseDeveloperExceptionPage();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
