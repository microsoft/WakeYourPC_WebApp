using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WakeYourPcWebApp.Models
{
    public class WakeupContext : DbContext
    {
        private readonly IConfigurationRoot _configuration;
        private ILogger _logger;

        public WakeupContext(IConfigurationRoot configuration,
            DbContextOptions dbContextOptions,
            ILogger<WakeupContext> logger)
            : base(dbContextOptions)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Machine> Machines { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var connString = _configuration["ConnectionStrings:DefaultConnection"];
            optionsBuilder.UseSqlServer(connString);
        }
    }
}
