using IdentityServer.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.DataBase.Contexts
{
    /// <summary>
    /// Коннектор с базой данных
    /// </summary>
    public class AppIdentityDbContext : IdentityDbContext<AuthIdentityUser, AuthIdentityRole, int, AuthIdentityUserClaim, AuthIdentityUserRole, AuthIdentityUserLogin, AuthIdentityRoleClaim, AuthIdentityUserToken>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AuthIdentityUser>(entity =>
                entity.ToTable(name: "Users"));
            builder.Entity<AuthIdentityRole>(entity =>
                entity.ToTable(name: "Roles"));
            builder.Entity<AuthIdentityUserRole>(entity =>
                entity.ToTable(name: "UserRoles"));
            builder.Entity<AuthIdentityUserClaim>(entity =>
                entity.ToTable(name: "UserClaim"));
            builder.Entity<AuthIdentityUserLogin>(entity =>
                entity.ToTable("UserLogins"));
            builder.Entity<AuthIdentityUserToken>(entity =>
                entity.ToTable("UserTokens"));
            builder.Entity<AuthIdentityRoleClaim>(entity =>
                entity.ToTable("RoleClaims"));
        }
    }
}
