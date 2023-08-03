﻿using CarRentalManagerAPI.Entities;
using CarRentalManagerAPI.Enums;
using System.Collections.Generic;

namespace CarRentalManagerAPI.Models.Car
{
    public class CarDto
    {
        public int Id { get; set; }
        public string Mark { get; set; }
        public string Model { get; set; }
        public TransmissionEnum Transmission { get; set; }
        public int EnginePower { get; set; }
        public DrivingLicenseCategoryEnum DrivingLicenseCategory { get; set; }
        public int NumberOfSeats { get; set; }
        public int PricePerDay { get; set; }
        public string RegistrationNumber { get; set; }
        public string VIN { get; set; }
        public CarStatusEnum Status { get; set; }
        public string Comments { get; set; }
    }
}
