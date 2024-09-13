using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class PodBookingSystemDbContext : DbContext
    {
        public PodBookingSystemDbContext()
        {

        }
        public PodBookingSystemDbContext(DbContextOptions<PodBookingSystemDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("connectionstrings.json", true, true)
                    .Build();
                var connectionString = configuration.GetConnectionString("PodBookingSystemDB");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        //DbSet for custom entities
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
