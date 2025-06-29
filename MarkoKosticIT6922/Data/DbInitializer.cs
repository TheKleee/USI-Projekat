using MarkoKosticIT6922.Constants;
using MarkoKosticIT6922.Models;
using Microsoft.AspNetCore.Identity;

namespace MarkoKosticIT6922.Data
{
    public class DbInitializer
    {
        public static async Task SeedAsync(IServiceProvider service)
        {
            var userManager = service.GetService<UserManager<Korisnik>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>();
            var context = service.GetRequiredService<ApplicationDbContext>();


            await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));

            await AdminUserSeeder.SeedAsync(userManager, roleManager);
            await IgreSeeder.SeedAsync(context);
            await UlogeSeeder.SeedAsync(context);
            await ZadaciSeeder.SeedAsync(context);
        }
    }
}
