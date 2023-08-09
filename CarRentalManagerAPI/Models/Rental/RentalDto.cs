using CarRentalManagerAPI.Entities;
using CarRentalManagerAPI.Enums;
using CarRentalManagerAPI.Models.Car;
using CarRentalManagerAPI.Models.Client;
using CarRentalManagerAPI.Models.User;
using System;
using System.Collections.Generic;

namespace CarRentalManagerAPI.Models.Rental
{
   public class RentalDto
    {
        public int Id { get; set; }
        public CarDto Car { get; set; }
        public ClientDto Client { get; set; }
        public UserDto User { get; set; }
        public string RentalDate { get; set; }
        public string ExpectedDateOfReturn { get; set; }
        public string Status { get; set; }
        public double Amount { get; set; }
        public string Comments { get; set; }
        public string DateOfReturn { get; set; }
    }
}
