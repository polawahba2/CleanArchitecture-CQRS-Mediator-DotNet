
using Domain.Constants;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Seed
{
    public class ApplicationDBContextSeed
    {
        public static async Task SeedUserAsync(UserManager<ApplicationUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var newUser = new ApplicationUser()
                {
                    UserName = "pola",
                    Email = "pola@gmail.com",
                    PhoneNumber = "01012000000",
                    Role = Roles.User,
                };

                await userManager.CreateAsync(newUser, "Pa$$w0rd");
            }
        }

        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            // Seed Admin Role
            if (!await roleManager.RoleExistsAsync(Roles.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole
                {
                    Name = Roles.Admin,
                    NormalizedName = Roles.Admin.ToUpper()
                });
            }

            // Seed User Role
            if (!await roleManager.RoleExistsAsync(Roles.User))
            {
                await roleManager.CreateAsync(new IdentityRole
                {
                    Name = Roles.User,
                    NormalizedName = Roles.User.ToUpper()
                });
            }
        }
    }
}