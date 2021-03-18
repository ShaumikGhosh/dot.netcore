using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TelephoneApp.Models;

namespace TelephoneApp.Data
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options){}
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<PhonebookModel> PhonebookRecords { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            const string ADMIN_ID = "a18be9c0-aa65-4af8-bd17-00bd9450e575";
            const string ROLE_ID = "a18be9c0-aa65-4af8-bd17-00bd9450e900";


            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = ROLE_ID, Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Name = "User", NormalizedName = "USER" }
            );

            var hasher = new PasswordHasher<ApplicationUser>();

            builder.Entity<ApplicationUser>().HasData(

                new ApplicationUser
                {
                    Id = ADMIN_ID,
                    FirstName = "Super",
                    LastName = "User",
                    UserName = "admin@dotnet.project",
                    NormalizedUserName = "admin@dotnet.project".ToUpper(),
                    Email = "admin@dotnet.project",
                    NormalizedEmail = "admin@dotnet.project".ToUpper(),
                    PhoneNumber = "01685987563",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Admin123."),
                }
            ); ;

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });
        }
    }
}
