using MarkoKosticIT6922.Constants;
using MarkoKosticIT6922.Models;
using Microsoft.AspNetCore.Identity;

namespace MarkoKosticIT6922.Data
{
    internal class AdminUserSeeder
    {
        internal static async Task SeedAsync(UserManager<Korisnik>? userManager, RoleManager<IdentityRole>? roleManager)
        {
            if (roleManager != null && userManager != null)
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));

                if (await userManager.FindByEmailAsync("admin@raf.rs") == null)
                {
                    var user = new Korisnik
                    {
                        Ime = "Admin",
                        UserName = "admin@raf.rs",
                        Email = "admin@raf.rs",
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        Admin = true
                    };
                    await userManager.CreateAsync(user, "Admin123$");
                    await userManager.AddToRoleAsync(user, Roles.Admin.ToString());
                }
            }
        }
    }
}