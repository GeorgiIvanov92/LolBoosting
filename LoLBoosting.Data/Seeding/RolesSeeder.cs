using LolBoosting.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoLBoosting.Data.Seeding
{
    internal class RolesSeeder : ISeeder
    {
        public async Task SeedAsync(LolBoostingDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var roles = new string[] { "Administrator", "Client", "Booster" };
            await SeedRolesAsync(roleManager, roles);
        }

        private async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager, ICollection<string> roleNames)
        {
            foreach (var roleName in roleNames)
            {
                var role = await roleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    var result = await roleManager.CreateAsync(new IdentityRole(roleName));
                    if (!result.Succeeded)
                    {
                        throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                    }
                }
            }
        }
    }
}
