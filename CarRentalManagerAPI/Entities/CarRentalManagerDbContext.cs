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
            //Car
            modelBuilder.Entity<Car>()
                .Property(c => c.Status)
                .IsRequired();
            modelBuilder.Entity<Car>()
                .Property(c => c.Comments)
                .HasMaxLength(50);
            modelBuilder.Entity<Car>()
                .Property(c => c.Transmission)
                .IsRequired();
            modelBuilder.Entity<Car>()
                .Property(c => c.Mark)
                .HasMaxLength(25)
                .IsRequired();
            modelBuilder.Entity<Car>()
                .Property(c => c.DrivingLicenseCategory)
                .IsRequired();
            modelBuilder.Entity<Car>()
                .Property(c => c.EnginePower)
                .HasMaxLength(4)
                .IsRequired();
            modelBuilder.Entity<Car>()
                .Property(c => c.Model)
                .HasMaxLength(25)
                .IsRequired();
            modelBuilder.Entity<Car>()
                .Property(c => c.NumberOfSeats)
                .HasMaxLength(2)
                .IsRequired();
            modelBuilder.Entity<Car>()
                .Property(c => c.PricePerDay)
                .HasMaxLength(6)
                .IsRequired();
            modelBuilder.Entity<Car>()
                .Property(c => c.RegistrationNumber)
                .HasMaxLength(10)
                .IsRequired();
            modelBuilder.Entity<Car>()
                .Property(c => c.VIN)
                .HasMaxLength(25)
                .IsRequired();

            //Client
            modelBuilder.Entity<Client>()
                .Property(c=>c.Surname)
                .HasMaxLength(25)
                .IsRequired();
            modelBuilder.Entity<Client>()
                .Property(c => c.IsBlocked)
                .IsRequired();
            modelBuilder.Entity<Client>()
                .Property(c => c.Comments)
                .HasMaxLength(50);
            modelBuilder.Entity<Client>()
                .Property(c => c.DrivingLicenseCategory)
                .IsRequired();
            modelBuilder.Entity<Client>()
                .Property(c => c.Email)
                .HasMaxLength(25)
                .IsRequired();
            modelBuilder.Entity<Client>()
                .Property(c => c.Name)
                .HasMaxLength(25)
                .IsRequired();
            modelBuilder.Entity<Client>()
                .Property(c => c.PESELOrPassportNumber)
                .HasMaxLength(25)
                .IsRequired();
            modelBuilder.Entity<Client>()
                .Property(c => c.PhoneNumber)
                .HasMaxLength(9)
                .IsRequired();

            //Rental
            modelBuilder.Entity<Rental>()
                .Property(r => r.Status)
                .IsRequired();
            modelBuilder.Entity<Rental>()
                .Property(r => r.Amount)
                .HasMaxLength(25)
                .IsRequired();
            modelBuilder.Entity<Rental>()
                .Property(r => r.Comments)
                .HasMaxLength(50);
            modelBuilder.Entity<Rental>()
                .Property(r => r.ExpectedDateOfReturn)
                .IsRequired();
            modelBuilder.Entity<Rental>()
                .Property(r => r.RentalDate)
                .IsRequired();

            //User
            modelBuilder.Entity<User>()
                .Property(u => u.Surname)
                .HasMaxLength(25)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.Name)
                .HasMaxLength(25)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.PhoneNumber)
                .HasMaxLength(9)
                .IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
