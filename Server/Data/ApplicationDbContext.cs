using ClassTracking.Server.Models;
using ClassTracking.Shared.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassTracking.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>();

            builder.Entity<ApplicationUser>().HasData(new ApplicationUser { UserName = "admin@gmail.com", NormalizedUserName = "ADMIN@GMAIL.COM", Email = "admin@gmail.com", NormalizedEmail = "ADMIN@GMAIL.COM", EmailConfirmed = true, PasswordHash = "AQAAAAEAACcQAAAAEL+DUqR3kfTbEmOrV9+A0hR2A9+iUbuBfRR/aaP/LKZkr/MpH//QrV3fMHH95kKyVw==", SecurityStamp = "R2T6KH3SFULI6OMKR7I6RFUTKNOX36EI", ConcurrencyStamp = "6f049186-a020-4547-a912-bf4c47b50520", PhoneNumber = "01917698460", PhoneNumberConfirmed = false, TwoFactorEnabled = false, LockoutEnabled = true, AccessFailedCount = 0, Id = "1d3a6793-0b53-412c-9d8d-5ea6a3b2e6f3" });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN", Id = "47e0f05c-104c-4a6e-9352-1205da7b622f", ConcurrencyStamp = Guid.NewGuid().ToString() });
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { UserId = "1d3a6793-0b53-412c-9d8d-5ea6a3b2e6f3", RoleId = "47e0f05c-104c-4a6e-9352-1205da7b622f" });
        }

        //Start Class Info
        public DbSet<Class> Classes { get; set; }
        //End Class Info
        //Start Student Info
        public DbSet<Student> Students { get; set; }
        //End Student Info
        //Start Teacher Info
        public DbSet<Teacher> Teachers { get; set; }
        //End Teacher Info
        //Start Enroll Student
        public DbSet<EnrollStudent> EnrollStudents { get; set; }
        //End Enroll Student
        //Start Assign Teacher
        public DbSet<AssignTeacher> AssignTeachers { get; set; }
        //End Assign Teacher
    }
}
