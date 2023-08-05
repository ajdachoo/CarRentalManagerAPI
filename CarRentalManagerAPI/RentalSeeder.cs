using CarRentalManagerAPI.Entities;
using CarRentalManagerAPI.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CarRentalManagerAPI
{
    public class RentalSeeder
    {
        private readonly CarRentalManagerDbContext _dbContext;

        public RentalSeeder(CarRentalManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Clients.Any())
                {
                    var clients = GetClients();
                    _dbContext.Clients.AddRange(clients);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Cars.Any())
                {
                    var cars = GetCars();
                    _dbContext.Cars.AddRange(cars);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Users.Any())
                {
                    var users = GetUsers();
                    _dbContext.Users.AddRange(users);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Client> GetClients()
        {
            var clients = new List<Client>()
            {
                new Client()
                {
                    Name = "Jan",
                    Surname = "Kowalski",
                    DrivingLicenseCategories = "B",
                    Email = "jankowalski@gmail.com",
                    PESELOrPassportNumber = "11111111111",
                    PhoneNumber = "000000000",
                    IsBlocked = false,
                }
            };

            return clients;
        }

        private IEnumerable<Car> GetCars()
        {
            var cars = new List<Car>()
            {
                new Car()
                {
                    Mark = "Toyota",
                    Model = "Corolla",
                    DrivingLicenseCategory = DrivingLicenseCategoryEnum.B,
                    EnginePower = 200,
                    NumberOfSeats = 5,
                    PricePerDay = 150,
                    Status = CarStatusEnum.Avaliable,
                    RegistrationNumber = "WWW123",
                    VIN = "12345678qwertyui",
                    Comments = "ładny",
                    Transmission = TransmissionEnum.Authomatic,

                }
            };

            return cars;
        }

        private IEnumerable<User> GetUsers()
        {
            var users = new List<User>()
            {
                new User()
                {
                    Name = "Janusz",
                    Surname = "Nosacz",
                    PhoneNumber = "999999999",
                }
            };

            return users;
        }
    }
}
