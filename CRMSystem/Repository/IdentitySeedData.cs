using CRMSystem.Models;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace CRMSystem.Repository
{
    public class IdentitySeedData
    {
        private const string _adminLogin = "Admin";
        private const string _adminPassword = "Password123!";
        private const string _managerLogin1 = "Manager1";
        private const string _managerPassword1 = "manager_password1";
        private const string _managerLogin2 = "Manager2";
        private const string _managerPassword2 = "manager_password2";

        public static async Task EnsurePopulatedAsync(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                // Создание ролей
                await CreateRoleIfNotExists(roleManager, Role.Admin);
                await CreateRoleIfNotExists(roleManager, Role.Manager);

                // Создание администратора
                await CreateUserIfNotExists(userManager, _adminLogin, _adminPassword, Role.Admin);

                // Создание менеджеров
                await CreateUserIfNotExists(userManager, _managerLogin1, _managerPassword1, Role.Manager);
                await CreateUserIfNotExists(userManager, _managerLogin2, _managerPassword2, Role.Manager);
            }
        }

        private static async Task CreateRoleIfNotExists(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        private static async Task CreateUserIfNotExists(UserManager<IdentityUser> userManager, string login, string password, string role)
        {
            var user = await userManager.FindByNameAsync(login);
            if (user == null)
            {
                user = new IdentityUser { UserName = login };
                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
                else
                {

                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"Error creating user {login}: {error.Description}");
                    }
                }
            }
        }
    }

}