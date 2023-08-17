using AutoMapper;
using CarRentalManagerAPI.Entities;
using CarRentalManagerAPI.Enums;
using CarRentalManagerAPI.Exceptions;
using CarRentalManagerAPI.Models.Car;
using CarRentalManagerAPI.Models.Client;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CarRentalManagerAPI.Services
{
    public interface IClientService
    {
        public IEnumerable<ClientDto> GetAll(bool? isBlocked);
        public int Create(CreateClientDto createClientDto);
        public ClientDto GetById(int id);
        public void Delete(int id);
        public void Update(int id, UpdateClientDto updateClientDto);
    }
    public class ClientService : IClientService
    {
        private readonly CarRentalManagerDbContext _dbContext;
        private readonly IMapper _mapper;

        public ClientService(CarRentalManagerDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IEnumerable<ClientDto> GetAll(bool? isBlocked)
        {
            var clients = _dbContext
                .Clients
                .Where(c=> isBlocked == null || c.IsBlocked == isBlocked)
                .ToList();

            var clientsDtos = _mapper.Map<List<ClientDto>>(clients);

            return clientsDtos;
        }

        public int Create(CreateClientDto createClientDto)
        {
            var client = _mapper.Map<Client>(createClientDto);

            _dbContext.Clients.Add(client);
            _dbContext.SaveChanges();

            return client.Id;
        }

        public ClientDto GetById(int id)
        {
            var client = _dbContext.Clients.FirstOrDefault(c => c.Id == id);

            if (client is null) throw new NotFoundException("Client not found");

            var clientDto = _mapper.Map<ClientDto>(client);

            return clientDto;
        }

        public void Delete(int id)
        {
            var client = _dbContext.Clients.FirstOrDefault(c => c.Id == id);

            if (client is null) throw new NotFoundException("Client not found");

            _dbContext.Clients.Remove(client);
            _dbContext.SaveChanges();
        }

        public void Update(int id, UpdateClientDto updateClientDto)
        {
            var client = _dbContext.Clients.FirstOrDefault(c => c.Id == id);

            if (client is null) throw new NotFoundException("Client not found");

            var updateClient = _mapper.Map<Client>(updateClientDto);

            var emailInUse = _dbContext.Clients.Any(c => c.Email == updateClient.Email);
            if (emailInUse)
            {
                if (client.Email != updateClient.Email) throw new ValueIsTakenException("That email is taken");
            }

            var peselOrPassportNumber = _dbContext.Clients.Any(c => c.PESELOrPassportNumber == updateClient.PESELOrPassportNumber);
            if (peselOrPassportNumber)
            {
                if (client.PESELOrPassportNumber != updateClient.PESELOrPassportNumber) throw new ValueIsTakenException("That pesel/passport number is taken");
            }

            var phoneNumberInUse = _dbContext.Clients.Any(c => c.PhoneNumber == updateClient.PhoneNumber);
            if (phoneNumberInUse)
            {
                if (client.PhoneNumber != updateClient.PhoneNumber) throw new ValueIsTakenException("That phone number is taken");
            }

            client.Surname = updateClient.Surname;
            client.PhoneNumber = updateClient.PhoneNumber;
            client.Email = updateClient.Email;
            client.DrivingLicenseCategories = updateClient.DrivingLicenseCategories;
            client.PESELOrPassportNumber = updateClient.PESELOrPassportNumber;
            client.Comments = updateClient.Comments;
            client.IsBlocked = updateClient.IsBlocked;
            client.Name = updateClient.Name;

            _dbContext.SaveChanges();
        }


    }
}
