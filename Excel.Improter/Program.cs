using Excel.Improter.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excel.Improter
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var container = host.Services.CreateScope())
            {
                var userManager = container.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var roleManager = container.ServiceProvider.GetRequiredService<RoleManager<Role>>();
                if (!roleManager.Roles.Any())
                {
                    var admin = new Role
                    {
                        Name = "Administrator",
                        FullName = "Administrator"
                    };
                    var expert = new Role
                    {
                        FullName = "Geoloji ekspertiza və reyestr şöbəsi",
                        Name = "Expert"
                    };
                    var marketolog = new Role
                    {
                        FullName = "Maliyyə, strategiya və marketinq",
                        Name = "Marketolog"
                    };
                    var supporter = new Role
                    {
                        FullName = "Yer təki fəaliyyətinin tənzimlənməsi",
                        Name = "Supporter"
                    };
                    await roleManager.CreateAsync(admin);
                    await roleManager.CreateAsync(marketolog);
                    await roleManager.CreateAsync(expert);
                    await roleManager.CreateAsync(supporter);
                }
                if (!userManager.Users.Any())
                {
                    AppUser user = new AppUser()
                    {
                        UserName = "Admin"
                    };
                    var result = await userManager.CreateAsync(user, "Admin123!");
                    if (result.Succeeded)
                    {
                        var addedUser = await userManager.FindByNameAsync("Admin");
                        await userManager.AddToRoleAsync(user, "Administrator");
                    }
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
