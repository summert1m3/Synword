using Microsoft.AspNetCore.Identity;
using Synword.Domain.Enums;
using Synword.Persistence.Entities.Identity;

namespace Synword.Persistence.Identity;

public static class AppIdentityDbContextSeed
{
    public static async Task SeedAsync(
        UserManager<UserIdentity> userManager, RoleManager<IdentityRole> roleManager)
    {
        await roleManager.CreateAsync(new IdentityRole(Role.Guest.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Role.User.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Role.Admin.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Role.Silver.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Role.Gold.ToString()));
    }
}
