using MeatAndMoreAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeatAndMoreAPI.Data
{
    public class MeatAndMoreContext : IdentityDbContext
        <
        User,
        Role,
        string,
        IdentityUserClaim<string>,
        UserRoles,
        IdentityUserLogin<string>,
        IdentityRoleClaim<string>,
        IdentityUserToken<string>
        >
    {
        public MeatAndMoreContext(DbContextOptions<MeatAndMoreContext> options) : base(options) { }

        public DbSet<LoggedVisitor> LoggedVisitors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            if (modelBuilder == null) { throw new ArgumentNullException(nameof(modelBuilder)); }


            // Fluent API Configuration
            // ========================

            // Users
            // =====
            modelBuilder.Entity<User>(u =>
            {
                u.HasMany(x => x.UserRoles)
                .WithOne(x => x.User)
                .HasForeignKey(y => y.UserId)
                .IsRequired();
            });

            // Roles
            // =====
            modelBuilder.Entity<Role>(r =>
            {
                r.HasMany(x => x.UserRoles)
                .WithOne(x => x.Role)
                .HasForeignKey(y => y.RoleId)
                .IsRequired();
            });
        }
    }
}
