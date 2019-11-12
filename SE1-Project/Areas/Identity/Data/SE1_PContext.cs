using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SE1_Project.Areas.Identity.Data;

namespace SE1_Project.Models
{
    public class SE1_PContext : IdentityDbContext<SE1_ProjectUser>
    {
        public SE1_PContext(DbContextOptions<SE1_PContext> options)
            : base(options)
        {
        }

        public DbSet<SE1_ProjectUser> SE1_ProjectUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
        //public DbSet<SE1_ProjectUser> ApplicationUser { get; set; }
    }
   
}
