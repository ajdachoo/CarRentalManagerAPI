using AutoMapper;
using CarRentalManagerAPI.Entities;
using CarRentalManagerAPI.Exceptions;
using CarRentalManagerAPI.Models.Client;
using CarRentalManagerAPI.Models.User;
using System.Collections.Generic;
using System.Linq;

namespace CarRentalManagerAPI.Services
{
    public interface IUserService
    {
        public IEnumerable<UserDto> GetAll();
        public int Create(CreateUserDto createUserDto);
        public UserDto GetById(int id);
        public void Delete(int id);
        public void Update(int id, UpdateUserDto updateUserDto);
    }
    
    public class UserService : IUserService
    {
        private readonly CarRentalManagerDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserService(CarRentalManagerDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IEnumerable<UserDto> GetAll()
        {
            var users = _dbContext.Users.ToList();

            var usersDtos = _mapper.Map<List<UserDto>>(users);

            return usersDtos;
        }

        public int Create(CreateUserDto createUserDto)
        {
            var user = _mapper.Map<User>(createUserDto);

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return user.Id;
        }

        public UserDto GetById(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(c => c.Id == id);

            if (user is null) throw new NotFoundException("User not found");

            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }

        public void Delete(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(c => c.Id == id);

            if (user is null) throw new NotFoundException("User not found");

            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
        }

        public void Update(int id, UpdateUserDto updateUserDto)
        {
            var user = _dbContext.Users.FirstOrDefault(c => c.Id == id);

            if (user is null) throw new NotFoundException("User not found");

            var updateUser = _mapper.Map<User>(updateUserDto);

            var phoneNumberInUse = _dbContext.Users.Any(c => c.PhoneNumber == updateUser.PhoneNumber);
            if (phoneNumberInUse)
            {
                if (user.PhoneNumber != updateUser.PhoneNumber) throw new ValueIsTakenException("That phone number is taken");
            }

            user.Surname = updateUser.Surname;
            user.PhoneNumber = updateUser.PhoneNumber;
            user.Name = updateUser.Name;

            _dbContext.SaveChanges();
        }
    }
}
