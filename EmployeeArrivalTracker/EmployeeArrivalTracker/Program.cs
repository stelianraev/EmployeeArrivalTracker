using EmployeeArrivalTracker.Configuration;
using EmployeeArrivalTracker.Data;
using EmployeeArrivalTracker.Infrastructure;
using EmployeeArrivalTracker.Services;
using EmployeeArrivalTracker.Services.EmployeeArrival;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var serviceConfig = builder.Configuration.GetSection("ServiceConfig");

// Add services to the container.
builder.Services.Configure<ServiceConfiguration>(serviceConfig);
builder.Services.AddScoped<IEmployeeDataExtractionService, EmployeeDataExtractionService>();
builder.Services.AddScoped<ComunicationService>();
builder.Services.AddDbContext<EmployeeArrivalTrackerDbContext>(opt =>
    opt.EnableSensitiveDataLogging()
    .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.PrepareDatabase();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection()
    .UseStaticFiles()
    .UseRouting()
    .UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
