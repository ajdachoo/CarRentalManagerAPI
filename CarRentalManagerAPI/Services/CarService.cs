﻿using AutoMapper;
using CarRentalManagerAPI.Entities;
using CarRentalManagerAPI.Models.Car;
using System.Collections.Generic;
using System.Linq;

namespace CarRentalManagerAPI.Services
{
    public interface ICarService
    {
        CarDto GetById(int id);
        IEnumerable<CarDto> GetAll();
        int Create(CreateCarDto createCarDto);
        bool Delete(int id);
        bool Update(int id, UpdateCarDto updateCarDto);
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

        public bool Update(int id, UpdateCarDto updateCarDto)
        {
            var car = _dbContext.Cars.FirstOrDefault(c => c.Id == id);

            if(car is null) return false;

            var updateCar = _mapper.Map<Car>(updateCarDto); 

            car.PricePerDay = updateCar.PricePerDay;
            car.DrivingLicenseCategory = updateCar.DrivingLicenseCategory;
            car.EnginePower = updateCar.EnginePower;
            car.Transmission = updateCar.Transmission;
            car.Status = updateCar.Status;
            car.Mark = updateCar.Mark;
            car.VIN = updateCar.VIN;
            car.RegistrationNumber = updateCar.RegistrationNumber;
            car.Comments = updateCar.Comments;
            car.Model = updateCar.Model;
            car.NumberOfSeats = updateCar.NumberOfSeats;

            _dbContext.SaveChanges();

            return true;
        }

        public bool Delete(int id)
        {
            var car = _dbContext.Cars.FirstOrDefault(c => c.Id == id);

            if(car is null) return false;

            _dbContext.Cars.Remove(car);
            _dbContext.SaveChanges();

            return true;
        }

        public CarDto GetById(int id)
        {
            var car = _dbContext.Cars.FirstOrDefault(c => c.Id == id);

            if (car is null) return null;

            var carDto = _mapper.Map<CarDto>(car);

            return carDto;
        }

        public IEnumerable<CarDto> GetAll()
        {
            var cars = _dbContext.Cars.ToList();

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
