using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Schoolager.Web.Data.Entities;
using Schoolager.Web.Helpers;
using System;
using System.Threading.Tasks;

namespace Schoolager.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.MigrateAsync();

            await AddAdminUser();
        }

        private async Task AddAdminUser()
        {
            var user = await _userHelper.GetUserByEmailAsync("admin@schoolager.com");

            if (user == null)
            {
                user = new User
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    Email = "admin@schoolager.com",
                    UserName = "admin@schoolager.com",
                    PhoneNumber = "214658973",
                };

                var result = await _userHelper.AddUserAsync(user, "123456");

                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder.");
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}
