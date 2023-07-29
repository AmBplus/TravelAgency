using AirlineReservationSystem.Infrastructure.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace AirlineReservationSystem.SeedData
{
  
    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MapSeedDataExtensions
    {
        public static async Task<WebApplication> UseMapSeedData(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {

                var _userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
               
                var _roleManger = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                await DatabaseSeedData.InitialSeedRole(roleManager: _roleManger);
                await DatabaseSeedData.InitialSeedUser(_userManager: _userManager);
            }
            return app;
        }
    }
}
