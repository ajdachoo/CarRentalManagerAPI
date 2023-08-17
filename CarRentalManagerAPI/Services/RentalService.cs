using AutoMapper;
using CarRentalManagerAPI.Entities;
using CarRentalManagerAPI.Enums;
using CarRentalManagerAPI.Exceptions;
using CarRentalManagerAPI.Models.Client;
using CarRentalManagerAPI.Models.Rental;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CarRentalManagerAPI.Services
{
    public interface IRentalService
    {
        public int Create(CreateRentalDto createRentalDto);
        public void Delete(int id);
        public double Finish(int id, FinishRentalDto finishRentalDto);
        public IEnumerable<RentalDto> GetAll(string status);
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

            if (rental.RentalDate > rental.ExpectedDateOfReturn)
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

        public double Finish(int id, FinishRentalDto finishRentalDto)
        {
            var dateOfReturn = DateTime.ParseExact(finishRentalDto.DateOfReturn, "yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture, DateTimeStyles.None);

            var rental = _dbContext.Rentals
                .Include(r => r.Car)
                .FirstOrDefault(r => r.Id == id);

            if (rental is null) throw new NotFoundException("Rental not found");
            if (rental.Status == RentalStatusEnum.Finished) throw new BadRequestException("Rental has already been finished");
            if (dateOfReturn < rental.RentalDate) throw new BadRequestException("Date of return must be later than rental date");

            rental.DateOfReturn = dateOfReturn;
            rental.Amount = CalculateAmount(rental);
            rental.Status = RentalStatusEnum.Finished;
            rental.Car.Status = CarStatusEnum.Avaliable;

            _dbContext.SaveChanges();

            return rental.Amount;
        }

        public IEnumerable<RentalDto> GetAll(string status)
        {
            RentalStatusEnum statusEnum = RentalStatusEnum.Active;

            if (status != null && !Enum.TryParse(status, true, out statusEnum))
                throw new BadRequestException("Invalid rental status");

            UpdateRentalsStatus();

            var rentals = _dbContext.Rentals
                .Include(r=>r.Car)
                .Include(r=>r.User)
                .Include(r=>r.Client)
                .Where(r => status == null || r.Status == statusEnum)
                .ToList();

            var rentalsDtos = _mapper.Map<List<RentalDto>>(rentals);

            return rentalsDtos;
        }

        public RentalDto GetById(int id)
        {
            UpdateRentalsStatus();

            var rental = _dbContext.Rentals
                .Include(r => r.Car)
                .Include(r => r.User)
                .Include(r => r.Client)
                .FirstOrDefault(c => c.Id == id);

            if (rental is null) throw new NotFoundException("Rental not found");

            var rentalDto = _mapper.Map<RentalDto>(rental);

            return rentalDto;
        }

        private double CalculateAmount(Rental rental)
        {
            double amount;
            double totalDays;

            if (rental.DateOfReturn is null || rental.DateOfReturn <= rental.ExpectedDateOfReturn)
            {
                totalDays = (rental.ExpectedDateOfReturn - rental.RentalDate).TotalDays;
            }
            else
            {
                totalDays = (rental.DateOfReturn.Value - rental.RentalDate).TotalDays;
            }

            amount = Math.Round(totalDays * rental.Car.PricePerDay, 2);

            return amount;
        }

        private void UpdateRentalsStatus()
        {
            DateTime nowDateTime = DateTime.Now;
            var rentals = _dbContext.Rentals
                .Where(r => r.ExpectedDateOfReturn < nowDateTime && r.DateOfReturn == null)
                .ToList();

            foreach(Rental rental in rentals)
            {
                rental.Status = RentalStatusEnum.Delayed;
            }

            _dbContext.SaveChanges();
        }
    }
}
