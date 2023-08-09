using AutoMapper;
using CarRentalManagerAPI.Entities;
using CarRentalManagerAPI.Enums;
using CarRentalManagerAPI.Exceptions;
using CarRentalManagerAPI.Models.Client;
using CarRentalManagerAPI.Models.Rental;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRentalManagerAPI.Services
{
    public interface IRentalService
    {
        public int Create(CreateRentalDto createRentalDto);
        public void Delete(int id);
        public double Finish(int id, DateTime dateOfReturn);
        public IEnumerable<RentalDto> GetAll();
        public RentalDto GetById(int id);
    }
    public class RentalService : IRentalService
    {
        private readonly CarRentalManagerDbContext _dbContext;
        private readonly IMapper _mapper;
        public RentalService(CarRentalManagerDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public int Create(CreateRentalDto createRentalDto)
        {
            var rental = _mapper.Map<Rental>(createRentalDto);

            if(rental.RentalDate > rental.ExpectedDateOfReturn)
            {
                throw new BadRequestException("Expected date of return must be later than rental date");
            }

            _dbContext.Rentals.Add(rental);

            rental.Status = RentalStatusEnum.Active;
            rental.Car.Status = CarStatusEnum.Rented;
            rental.Amount = CalculateAmount(rental);

            _dbContext.SaveChanges();

            return rental.Id;
        }

        public void Delete(int id)
        {
            var rental = _dbContext.Rentals.Include(r => r.Car).FirstOrDefault(r => r.Id == id);

            if (rental is null) throw new NotFoundException("Rental not found");

            rental.Car.Status = CarStatusEnum.Avaliable;
            
            _dbContext.Rentals.Remove(rental);
            _dbContext.SaveChanges();
        }

        public double Finish(int id, DateTime dateOfReturn)
        {
            var rental = _dbContext.Rentals
                .Include(r => r.Car)
                .FirstOrDefault(r => r.Id == id);

            if (rental is null) throw new NotFoundException("Rental not found");
            if (rental.Status == RentalStatusEnum.Finished) throw new BadRequestException("Rental has already been finished");

            rental.DateOfReturn = dateOfReturn;
            rental.Amount = CalculateAmount(rental);
            rental.Status = RentalStatusEnum.Finished;
            rental.Car.Status = CarStatusEnum.Avaliable;

            _dbContext.SaveChanges();

            return rental.Amount;
        }

        public IEnumerable<RentalDto> GetAll()
        {
            UpdateRentalsStatus();

            var rentals = _dbContext.Rentals
                .Include(r=>r.Car)
                .Include(r=>r.User)
                .Include(r=>r.Client)
                .ToList();

            var rentalsDtos = _mapper.Map<List<RentalDto>>(rentals);

            return rentalsDtos;
        }

        public RentalDto GetById(int id)
        {
            UpdateRentalsStatus();

            var rental = _dbContext.Rentals.FirstOrDefault(c => c.Id == id);

            if (rental is null) throw new NotFoundException("Rental not found");

            var rentalDto = _mapper.Map<RentalDto>(rental);

            return rentalDto;
        }

        private double CalculateAmount(Rental rental)
        {
            double amount;
            double totalDays;

            if (rental.DateOfReturn is null)
            {
                totalDays = (rental.ExpectedDateOfReturn - rental.RentalDate).TotalDays;
            }
            else
            {
                totalDays = (rental.DateOfReturn.Value - rental.RentalDate).TotalDays;
            }

            amount = totalDays * rental.Car.PricePerDay;

            return amount;
        }

        private void UpdateRentalsStatus()
        {
            DateTime nowDateTime = DateTime.Now;
            var rentals = _dbContext.Rentals
                .Where(r => r.ExpectedDateOfReturn < nowDateTime)
                .ToList();

            foreach(Rental rental in rentals)
            {
                rental.Status = RentalStatusEnum.Delayed;
            }

            _dbContext.SaveChanges();
        }
    }
}
