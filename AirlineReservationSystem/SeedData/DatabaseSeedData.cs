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
         internal static async  Task InitialSeedRole(RoleManager<IdentityRole> roleManager)
        {
          
            string[] roles = new string[] { UserConstants.Role.AdministratorRole
                ,UserConstants.Role.FlightManagerRole
                ,UserConstants.Role.FleetManagerRole 
                ,UserConstants.Role.User};

            foreach (string role in roles)
            {
                

                if (!roleManager.Roles.Any(r => r.Name == role))
                {
                   var result =  roleManager.CreateAsync(new IdentityRole(role)).Result;
                    
                    
                }
            }


        }
        internal  static async  Task InitialSeedUser( UserManager<ApplicationUser> _userManager )
        {

          List<UserDto> userDtos = UserDto.GetUserDtoList();
            foreach (var userdto in userDtos)
            {
                if(!_userManager.Users.Any(x => x.Email == userdto.Email))
                {
                    try
                    {

                        var user = CreateUser();
                        user.EmailConfirmed = true;
                        await _userManager.SetUserNameAsync(user, userdto.Email);
                        await _userManager.SetEmailAsync(user, user.Email);
                        IdentityResult result = await _userManager.CreateAsync(user, userdto.Password);
                        await _userManager.AddToRoleAsync(user, userdto.Role);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
           
            }

     

    
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
        public string Role { get; set; }
        
        public static List<UserDto> GetUserDtoList()
        {
            return new List<UserDto>()
            {
                new UserDto(){Email = $"{UserConstants.Role.User}@gmail.com",Password = "Te$t12356789",Role = UserConstants.Role.User},
                new UserDto(){Email = $"{UserConstants.Role.AdministratorRole}@gmail.com",Password = "Te$t12356789",Role=UserConstants.Role.AdministratorRole},
                new UserDto(){Email = $"{UserConstants.Role.FleetManagerRole}@gmail.com",Password = "Te$t12356789",Role = UserConstants.Role.FleetManagerRole},
                new UserDto(){Email = $"{UserConstants.Role.FlightManagerRole}@gmail.com",Password = "Te$t12356789",Role=UserConstants.Role.FlightManagerRole}
            };
        }
    }
}
