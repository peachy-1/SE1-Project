using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SE1_Project.Models;

[assembly: HostingStartup(typeof(SE1_Project.Areas.Identity.IdentityHostingStartup))]
namespace SE1_Project.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<SE1_Project_Context>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("SE1_Project_ContextConnection")));

                services.AddDefaultIdentity<IdentityUser>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<SE1_Project_Context>();
            });
        }
    }
}