using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using DxWand.Core.Entities;
using DxWand.Core.Enums;
using System;

namespace DxWand.Infrastructure.Data
{
    public static class DataSeed
    {
        public static void SeedRoles(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole 
                {
                    Id = "8b4671fd-1aaa-438c-9554-3b1d6987c06c",
                    Name = "Admin",
                    NormalizedName = "admin"
                },
                new IdentityRole
                {
                    Id = "9aca54f1-4296-43cc-a5ff-814a0f4d3c1d",
                    Name = "user",
                    NormalizedName = "user"
                });
        }
        public static void SeedAdminUser(this ModelBuilder modelBuilder)
        {
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            var adminUser = new ApplicationUser
            {
                Id = "3c948e49-b382-488e-8f9b-05377d91bf75",
                UserName = "admin@dxwand.com",
                Email = "admin@dxwand.com",
                BirthDate = DateTime.Parse("05/18/1993"),
                Gender = GenderEnum.Male,
                PasswordHash = passwordHasher.HashPassword(null, "12345678")
            };

            modelBuilder.Entity<ApplicationUser>().HasData(adminUser);
        }

        public static void SeedUserRoles(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> 
                { 
                    RoleId = "8b4671fd-1aaa-438c-9554-3b1d6987c06c",
                    UserId = "3c948e49-b382-488e-8f9b-05377d91bf75"
                }
            );
        }
    }
}
