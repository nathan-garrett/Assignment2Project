using Microsoft.AspNetCore.Identity;
using Assignment2Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2Project.Data
{
    public static class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUserModel> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Roles.IT_Manager.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.IT_Support.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Staff.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Student.ToString()));
        }
        public static async Task SeedSuperAdminAsync(UserManager<ApplicationUserModel> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUserModel
            {
                UserName= "admin@gmail.com",
                Email = "admin@gmail.com",
                FirstName = "Nathan",
                LastName = "Garrett",
                EmailConfirmed = false

            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Password!1");
                    await userManager.AddToRoleAsync(defaultUser, Roles.IT_Manager.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.IT_Support.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Staff.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Student.ToString());
                }

            }
        }
    }
   
}
