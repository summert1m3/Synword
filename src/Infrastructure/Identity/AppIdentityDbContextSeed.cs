using Microsoft.AspNetCore.Identity;
using Synword.Domain.Enums;

namespace Synword.Infrastructure.Identity;

public static class AppIdentityDbContextSeed
{
    public static async Task SeedAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        await roleManager.CreateAsync(new IdentityRole(Role.Guest.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Role.User.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Role.Admin.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Role.Silver.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Role.Gold.ToString()));
    }
}
