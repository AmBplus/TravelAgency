using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AirlineReservationSystem.Data;
using AirlineReservationSystem.Core;
using AirlineReservationSystem.Infrastructure.Models;
using AirlineReservationSystem.Infrastructure.Migrations;
using Azure.Core;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using System.Text;
using System;

namespace AirlineReservationSystem.SeedData
{
    public  static class DatabaseSeedData
    {
         private static IServiceProvider InitialSeedRole(this IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<TravelAgencyDbContext>();

            string[] roles = new string[] { UserConstants.Role.AdministratorRole
                ,UserConstants.Role.FlightManagerRole
                ,UserConstants.Role.FleetManagerRole 
                ,UserConstants.Role.User};

            foreach (string role in roles)
            {
                var roleStore = new RoleStore<IdentityRole>(context);

                if (!context.Roles.Any(r => r.Name == role))
                {
                    roleStore.CreateAsync(new IdentityRole(role));
                }
            }


            return serviceProvider;
        }
        private  static  IServiceProvider InitialSeedUser(this IServiceProvider serviceProvider)
        {

          
            var _userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            var _userStore = serviceProvider.GetService<IUserStore<ApplicationUser>>();
            var _emailStore = serviceProvider.GetService<IUserEmailStore<ApplicationUser>>();
            List<UserDto> userDtos = UserDto.GetUserDtoList();
            foreach (var userdto in userDtos)
            {
                if(!_userManager.Users.Any(x => x.Email == userdto.Email))
                {
                    try
                    {

                        var user = CreateUser();
                        _userStore.SetUserNameAsync(user, userdto.Email, CancellationToken.None).GetAwaiter();
                        _emailStore.SetEmailAsync(user, userdto.Email, CancellationToken.None).GetAwaiter();
                        IdentityResult r = _userManager.CreateAsync(user, userdto.Password).GetAwaiter().GetResult();
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
           
            }

     

            return serviceProvider;
        }
        public static IServiceProvider AddInitialSeedData(this IServiceProvider services,IConfiguration configuration)
        {

            services.InitialSeedRole();
            services.InitialSeedUser();

            return services;
        }
        private static ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
    }
      
    internal class UserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }    
        
        public static List<UserDto> GetUserDtoList()
        {
            return new List<UserDto>()
            {
                new UserDto(){Email = $"{UserConstants.Role.User}@gmail.com",Password = "Te$t12356789"},
                new UserDto(){Email = $"{UserConstants.Role.AdministratorRole}@gmail.com",Password = "Te$t12356789"},
                new UserDto(){Email = $"{UserConstants.Role.FleetManagerRole}@gmail.com",Password = "Te$t12356789"},
                new UserDto(){Email = $"{UserConstants.Role.FlightManagerRole}@gmail.com",Password = "Te$t12356789"}
            };
        }
    }
}
