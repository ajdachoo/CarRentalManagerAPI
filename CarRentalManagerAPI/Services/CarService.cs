using AutoMapper;
using CarRentalManagerAPI.Entities;
using CarRentalManagerAPI.Enums;
using CarRentalManagerAPI.Exceptions;
using CarRentalManagerAPI.Models.Car;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRentalManagerAPI.Services
{
    public interface ICarService
    {
        CarDto GetById(int id);
        IEnumerable<CarDto> GetAll(string status);
        int Create(CreateCarDto createCarDto);
        void Delete(int id);
        void Update(int id, UpdateCarDto updateCarDto);
    }
    public class CarService : ICarService
    {
        private readonly CarRentalManagerDbContext _dbContext;
        private readonly IMapper _mapper;

        public CarService(CarRentalManagerDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Update(int id, UpdateCarDto updateCarDto)
        {
            var car = _dbContext.Cars.FirstOrDefault(c => c.Id == id);

            if (car is null) throw new NotFoundException("Car not found");

            var updateCar = _mapper.Map<Car>(updateCarDto);

            var vinInUse = _dbContext.Cars.Any(c => c.VIN == updateCar.VIN);
            if (vinInUse)
            {
                if (car.VIN != updateCar.VIN) throw new ValueIsTakenException("That VIN is taken");
            }

            var registrationNumberInUse = _dbContext.Cars.Any(c => c.RegistrationNumber == updateCar.RegistrationNumber);
            if (registrationNumberInUse)
            {
                if (car.RegistrationNumber != updateCar.RegistrationNumber) throw new ValueIsTakenException("That registration number is taken");
            }

            car.VIN = updateCar.VIN;
            car.RegistrationNumber = updateCar.RegistrationNumber;
            car.PricePerDay = updateCar.PricePerDay;
            car.DrivingLicenseCategory = updateCar.DrivingLicenseCategory;
            car.EnginePower = updateCar.EnginePower;
            car.Transmission = updateCar.Transmission;
            car.Status = updateCar.Status;
            car.Mark = updateCar.Mark;
            car.Comments = updateCar.Comments;
            car.Model = updateCar.Model;
            car.NumberOfSeats = updateCar.NumberOfSeats;

            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var car = _dbContext.Cars.FirstOrDefault(c => c.Id == id);

            if(car is null) throw new NotFoundException("Car not found");

            _dbContext.Cars.Remove(car);
            _dbContext.SaveChanges();
        }

        public CarDto GetById(int id)
        {
            var car = _dbContext.Cars.FirstOrDefault(c => c.Id == id);

            if (car is null) throw new NotFoundException("Car not found");

            var carDto = _mapper.Map<CarDto>(car);

            return carDto;
        }

        public IEnumerable<CarDto> GetAll(string status)
        {
            CarStatusEnum statusEnum = CarStatusEnum.Avaliable;

            if (status != null && !Enum.TryParse(status, true, out statusEnum))
                throw new BadRequestException("Invalid car status");

            var cars = _dbContext
                .Cars
                .Where(c => status == null || c.Status == statusEnum)
                .ToList();

            var carsDtos = _mapper.Map<List<CarDto>>(cars);

            return carsDtos;
        }

        public int Create(CreateCarDto createCarDto)
        {
            var car = _mapper.Map<Car>(createCarDto);
            _dbContext.Cars.Add(car);
            _dbContext.SaveChanges();

            return car.Id;
        }
    }
}
