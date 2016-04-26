using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace Travel.Models
{
    public class WorldContext : DbContext
    {

        public WorldContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<Trip> Trips { get; set; }
        public DbSet<Trip> Stops { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connString = Startup.Configuration["Data:WorldContextConnection"];

            optionsBuilder.UseSqlServer(connString);

            base.OnConfiguring(optionsBuilder);
        }
    }
}
