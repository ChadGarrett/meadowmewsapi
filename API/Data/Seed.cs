using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using API.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        ///////////////
        // Interface //
        ///////////////

        public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            // Don't seed if there is already users
            if (await userManager.Users.AnyAsync()) return;

            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
            if (users == null) return;

            var roles = new List<AppRole>
            {
                new AppRole{Name=AppRoleType.Member.ToString()},
                new AppRole{Name=AppRoleType.Admin.ToString()},
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            foreach (var user in users)
            {
                user.UserName = user.UserName.ToLower();
                await userManager.CreateAsync(user, "Pa$$w0rd");
                await userManager.AddToRoleAsync(user, AppRoleType.Member.ToString());
            }

            var admin = new AppUser 
            {
                UserName = "admin",
            };

            await userManager.CreateAsync(admin, "Pa$$w0rd");

            await userManager.AddToRolesAsync(admin, new[] {AppRoleType.Admin.ToString()});
        }

        public static async Task SeedElectricityPurchases(DataContext context)
        {
            if (await context.electricityPurchases.AnyAsync()) return;
            
            var purchaseData = await System.IO.File.ReadAllTextAsync("Data/ElectricitySeedData.json");
            var purchases = JsonSerializer.Deserialize<List<ElectricityPurchase>>(purchaseData);
            if (purchases == null) return;

            await context.electricityPurchases.AddRangeAsync(purchases);

            await context.SaveChangesAsync();
        }

        public static async Task SeedProperties(DataContext context)
        {
            if (await context.properties.AnyAsync()) return;

            var propertyData = await System.IO.File.ReadAllTextAsync("Data/PropertySeedData.json");
            var properties = JsonSerializer.Deserialize<List<Property>>(propertyData);

            if (properties == null) return;

            await context.properties.AddRangeAsync(properties);

            await context.SaveChangesAsync();
        }
    }
}