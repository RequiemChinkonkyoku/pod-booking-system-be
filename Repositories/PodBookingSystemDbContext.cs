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
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Membership> Memberships { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<SelectedProduct> SelectedProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure one-to-many relationship
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)  // One user has one role
                .WithMany(r => r.Users)  // One role has many users
                .HasForeignKey(u => u.RoleId)  // Foreign key in User table
                .OnDelete(DeleteBehavior.Restrict); // Optional: Prevent cascading delete
            modelBuilder.Entity<User>()
                .HasOne(u => u.Membership)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.MembershipId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Booking>()
                .HasOne(u => u.User)
                .WithMany(r => r.Bookings)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<SelectedProduct>()
                .HasOne(u => u.Product)
                .WithMany(r => r.SelectedProducts)
                .HasForeignKey(u => u.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<SelectedProduct>()
                .HasOne(u => u.Booking)
                .WithMany(r => r.SelectedProducts)
                .HasForeignKey(u => u.BookingId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
