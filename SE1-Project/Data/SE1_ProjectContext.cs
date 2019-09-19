using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SE1_Project.Models;

namespace SE1_Project.Models
{
    public class SE1_ProjectContext : DbContext
    {
        public SE1_ProjectContext (DbContextOptions<SE1_ProjectContext> options)
            : base(options)
        {
        }

        public DbSet<SE1_Project.Models.Professional> Professional { get; set; }

        public DbSet<SE1_Project.Models.Customer> Customer { get; set; }

        public DbSet<SE1_Project.Models.Review> Review { get; set; }
    }
}
