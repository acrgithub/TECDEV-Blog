using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TECDEVBlog.Web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // add (user, Admin, SuperAdmin) roles

            var adminRoleId = "033c8b06-47ef-46f7-a0b4-09ea20888e0c";
            var superAdminRoleId = "53f9637b-3a4a-452e-a045-5a10929f539f";
            var userRoleId = "f5bde115-f9f2-472b-bf0d-439510ad301a";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin",
                    Id = superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User",
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            // SuperAdminUser
            var superAdminId = "82322e03-d5a4-41b0-954e-6cab8a5323f6";
            var superAdminUser = new IdentityUser
            {
                UserName = "superadmin@tecdev.com",
                Email = "superadmin@tecdev.com",
                NormalizedEmail = "superadmin@tecdev.com".ToUpper(),
                NormalizedUserName = "superadmin@tecdev.com".ToUpper(),
                Id = superAdminId,
            };

            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(superAdminUser, "RallyStickFire@124");

            builder.Entity<IdentityUser>().HasData(superAdminUser);

            // add all roels to the SuperAdminUser
            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = superAdminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = superAdminId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }
    }
}
