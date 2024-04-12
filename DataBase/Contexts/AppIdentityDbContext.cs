using IdentityServer.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.DataBase.Contexts
{
    /// <summary>
    /// Коннектор с базой данных
    /// </summary>
    public class AppIdentityDbContext : IdentityDbContext<BaseIdentityUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<BaseIdentityUser>(entity => entity.ToTable(name: "Users"));
            builder.Entity<IdentityRole>(entity => entity.ToTable(name: "Roles"));
            builder.Entity<IdentityUserRole<string>>(entity =>
                entity.ToTable(name: "UserRoles"));
            builder.Entity<IdentityUserClaim<string>>(entity =>
                entity.ToTable(name: "UserClaim"));
            builder.Entity<IdentityUserLogin<string>>(entity =>
                entity.ToTable("UserLogins"));
            builder.Entity<IdentityUserToken<string>>(entity =>
                entity.ToTable("UserTokens"));
            builder.Entity<IdentityRoleClaim<string>>(entity =>
                entity.ToTable("RoleClaims"));
        }
    }
}
