using MeatAndMoreAPI.Data;
using MeatAndMoreAPI.Models;
using MeatAndMoreAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeatAndMoreAPI.Helpers
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new MeatAndMoreContext(serviceProvider.GetRequiredService<DbContextOptions<MeatAndMoreContext>>());
            var _loggedVisitorRepository = serviceProvider.GetRequiredService<ILoggedVisitorRepository>();

            Role adminRole = new Role
            {
                Name = "admin",
                NormalizedName = "ADMIN",
                Description = "Administrator",
            };

            context.Roles.Add(adminRole);
            context.SaveChanges();

            User admin = new User
            {
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@meatandmore.be",
                NormalizedEmail = "ADMIN@MEATANDMORE.BE",
                PasswordHash = "AQAAAAEAACcQAAAAEGnZ5lOZswaDpwPzrRW4soLyOwZLWC5qnGN0bBGuRjSesualPnLY3/h+XRVCFeN/og==",
            };

            context.Users.Add(admin);
            context.SaveChanges();

            UserRoles adminAdminRole = new UserRoles
            {
                User = admin,
                Role = adminRole
            };
        }
    }
}
