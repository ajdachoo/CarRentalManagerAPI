using AutoMapper;
using CarRentalManagerAPI.Entities;
using CarRentalManagerAPI.Exceptions;
using CarRentalManagerAPI.Models.Client;
using CarRentalManagerAPI.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace CarRentalManagerAPI.Services
{
    public interface IUserService
    {
        public IEnumerable<UserDto> GetAll();
        public int RegisterUser(CreateUserDto createUserDto);
        public UserDto GetById(int id);
        public void Delete(int id);
        public void Update(int id, UpdateUserDto updateUserDto);

        public string GenerateJwt(LoginUserDto loginUserDto);
    }
    
    public class UserService : IUserService
    {
        private readonly CarRentalManagerDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public UserService(CarRentalManagerDbContext dbContext, IMapper mapper, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }

        public string GenerateJwt(LoginUserDto loginUserDto)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.PhoneNumber == loginUserDto.PhoneNumber);
            if(user is null)
            {
                throw new BadRequestException("Invalid phone number or password");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginUserDto.Password);
            if(result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid phone number or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.Name} {user.Surname}"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        public IEnumerable<UserDto> GetAll()
        {
            var users = _dbContext.Users.ToList();

            var usersDtos = _mapper.Map<List<UserDto>>(users);

            return usersDtos;
        }

        public int RegisterUser(CreateUserDto createUserDto)
        {
            var user = _mapper.Map<User>(createUserDto);
            user.PasswordHash = _passwordHasher.HashPassword(user, createUserDto.Password);

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
