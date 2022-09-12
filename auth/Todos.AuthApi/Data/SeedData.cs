using Microsoft.AspNetCore.Identity;

namespace Todos.AuthApi.Data;

public static class SeedData
{
    public static async Task EnsureSeedData(IdentitiesDbContext context, IServiceScope scope)
    {
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        await EnsureSeedUsers(userManager, roleManager);
    }

    private static async Task EnsureSeedUsers(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        var userSearchResult = await roleManager.FindByNameAsync("User");
        if (userSearchResult == null)
        {
            var userRole = new IdentityRole
            {
                Name = "User"
            };

            await roleManager.CreateAsync(userRole);
        }

        var adminSearchResult = await roleManager.FindByNameAsync("Admin");
        if (adminSearchResult == null)
        {
            var adminRole = new IdentityRole
            {
                Name = "Admin"
            };

            await roleManager.CreateAsync(adminRole);
        }

        var user = new IdentityUser
        {
            UserName = "Guest12345",
            Email = "Guest12345@gmail.com",
            EmailConfirmed = false
        };

        var foundUser = await userManager.FindByNameAsync(user.UserName);
        if (foundUser is null)
        {
            const string password = "Guest12345";
            _ = await userManager.CreateAsync(user, password);

            foundUser = await userManager.FindByNameAsync(user.UserName);
            _ = await userManager.AddToRoleAsync(foundUser, "User");
        }

        var admin = new IdentityUser
        {
            UserName = "Admin12345",
            Email = "Admin12345@gmail.com",
            EmailConfirmed = false
        };

        var foundAdmin = await userManager.FindByNameAsync(admin.UserName);
        if (foundAdmin is null)
        {
            const string password = "Admin12345";
            _ = await userManager.CreateAsync(admin, password);

            foundAdmin = await userManager.FindByNameAsync(admin.UserName);
            _ = await userManager.AddToRoleAsync(foundAdmin, "Admin");
        }
    }
}
