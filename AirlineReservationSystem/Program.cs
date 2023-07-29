using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AirlineReservationSystem.Data;
using AirlineReservationSystem.Infrastructure.Models;
using AirlineReservationSystem.Extensions.Microsoft.Extensions.DependencyInjection;
using AirlineReservationSystem.SeedData;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplicationDbContexts(builder.Configuration);

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<TravelAgencyDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddMemoryCache();


//TODO: Implement redis caching 
//builder.Services.AddStackExchangeRedisCache(options =>
//{
//    options.Configuration = builder.Configuration.GetConnectionString("Redis");
//});

builder.Services.AddApplicationServices();
var app = builder.Build();
app.Services.AddInitialSeedData(app.Configuration);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
