using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace CarRentalManagerAPI.Entities
{
    public class CarRentalManagerDbContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<User> Users { get; set; }

        private string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=CarRentalManagerDb;Trusted_Connection=True;";

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(eb =>
            {
                eb.Property(c => c.Status).IsRequired();
                eb.Property(c => c.Transmission).IsRequired();
                eb.Property(c => c.DrivingLicenseCategory).IsRequired();
                eb.Property(c => c.Comments).HasMaxLength(50);
                eb.Property(c => c.Mark).HasMaxLength(25).IsRequired();
                eb.Property(c => c.EnginePower).IsRequired();
                eb.Property(c => c.Model).HasMaxLength(25).IsRequired();
                eb.Property(c => c.NumberOfSeats).IsRequired();
                eb.Property(c => c.PricePerDay).IsRequired();
                eb.Property(c => c.RegistrationNumber).HasMaxLength(10).IsRequired();
                eb.Property(c => c.VIN).HasMaxLength(25).IsRequired();
            });

            modelBuilder.Entity<Client>(eb =>
            {
                eb.Property(c => c.IsBlocked).IsRequired();
                eb.Property(c => c.DrivingLicenseCategories).IsRequired();
                eb.Property(c => c.Comments).HasMaxLength(50);
                eb.Property(c => c.Surname).HasMaxLength(25).IsRequired();
                eb.Property(c => c.Email).HasMaxLength(25).IsRequired();
                eb.Property(c => c.Name).HasMaxLength(25).IsRequired();
                eb.Property(c => c.PESELOrPassportNumber).HasMaxLength(25).IsRequired();
                eb.Property(c => c.PhoneNumber).HasMaxLength(9).IsRequired();
            });

            modelBuilder.Entity<Rental>(eb =>
            {
                eb.Property(r => r.Status).IsRequired();
                eb.Property(r => r.ExpectedDateOfReturn).IsRequired();
                eb.Property(r => r.RentalDate).IsRequired();
                eb.Property(r => r.Comments).HasMaxLength(50);
                eb.Property(r => r.Amount).IsRequired();
            });

            modelBuilder.Entity<User>(eb =>
            {
                eb.Property(u => u.Surname).HasMaxLength(25).IsRequired();
                eb.Property(u => u.Name).HasMaxLength(25).IsRequired();
                eb.Property(u => u.PhoneNumber).HasMaxLength(9).IsRequired();
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
