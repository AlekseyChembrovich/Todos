using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Todos.AuthApi.Data.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Todos.AuthApi.Data;

public class IdentitiesDbContext : IdentityDbContext
{
    public IdentitiesDbContext(DbContextOptions<IdentitiesDbContext> context) : base(context)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("auth");

        base.OnModelCreating(builder);

        builder.Entity<IdentityUser>(table => table.ToTable("Users"));
        builder.Entity<IdentityRole>(table => table.ToTable("Roles"));
        builder.Entity<IdentityUserRole<string>>(table => table.ToTable("UserRoles"));
        builder.Entity<IdentityUserClaim<string>>(table => table.ToTable("UserClaims"));
        builder.Entity<IdentityRoleClaim<string>>(table => table.ToTable("RoleClaims"));
        builder.Entity<IdentityUserLogin<string>>(table => table.ToTable("UserLogins"));
        builder.Entity<IdentityUserToken<string>>(table => table.ToTable("UserTokens"));

        builder.ApplyConfiguration(new IdentityConfiguration());
    }
}
