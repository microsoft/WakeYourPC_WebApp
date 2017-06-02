using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace WakeYourPcWebApp.Models
{
    public class WakeupContext : DbContext
    {
        private readonly IConfigurationRoot _configuration;

        public WakeupContext(IConfigurationRoot configuration, DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {
            _configuration = configuration;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Machine> Machines { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_configuration["ConnectionStrings:Sql"]);
        }
    }
}
