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
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Pod> Pods { get; set; }
        public virtual DbSet<PodType> PodTypes { get; set; }
        public virtual DbSet<Area> Areas { get; set; }

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
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Review)  // One booking has one review
                .WithOne(r => r.Booking)  // One review belongs to one booking
                .HasForeignKey<Review>(r => r.BookingId)  // Foreign key in Review table
                .OnDelete(DeleteBehavior.Cascade); // Optional: Specify cascading delete behavior
            modelBuilder.Entity<Pod>()
                .HasOne(u => u.PodType)
                .WithMany(r => r.Pods)
                .HasForeignKey(u => u.PodTypeId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Pod>()
                .HasOne(u => u.Area)
                .WithMany(r => r.Pods)
                .HasForeignKey(u => u.AreaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
