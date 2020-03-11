using LolBoosting.Constants;
using LolBoosting.Data.Context;
using LolBoosting.Models;
using LolBoostingConstants;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoLBoosting.Data.Seeding
{
    class UsersSeeder : ISeeder
    {
        public async Task SeedAsync(LolBoostingDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            if (!dbContext.Users.Any())
            {
                await SeedAdminAsync(userManager);
                await SeedClientsAsync(userManager, GenerateUsers(SeederConstants.ClientsToSeed));
            }
        }

        private async Task SeedAdminAsync(UserManager<User> userManager)
        {
            var admin = new User { UserName = "Admin", Email = "admin@admin.com", EmailConfirmed = true };

            var adminExists = await userManager.FindByNameAsync(admin.UserName);

            if (adminExists == null)
            {
                var identityResult = await userManager.CreateAsync(admin, SeederConstants.SeederAdminPassword);
                if (identityResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, GlobalConstants.AdministratorRoleName);
                }
            }
        }

        private async Task SeedClientsAsync(UserManager<User> userManager, IEnumerable<User> users)
        {
            foreach (var newUser in users)
            {
                var user = await userManager.FindByNameAsync(newUser.UserName);

                if (user == null)
                {
                    var identityResult = await userManager.CreateAsync(user, SeederConstants.SeederUserPassword);
                    if (identityResult.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, GlobalConstants.DefaultRole);
                    }
                }
            }
        }

        private IEnumerable<User> GenerateUsers(int count)
        {
            for (int i = 0; i <= count; i++)
            {
                var user = new User
                {
                    UserName = $"User{i}",
                    Email = $"{i}@mail.com",
                    EmailConfirmed = true,
                    Balance = 10 * i,
                };

                yield return user;
            }
        }
    }
}
