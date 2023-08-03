using CarRentalManagerAPI.Enums;
using System.Collections.Generic;

namespace CarRentalManagerAPI.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string Mark { get; set; }
        public string Model { get; set; }
        public TransmissionEnum Transmission { get; set; }
        public int EnginePower { get; set; }
        public DrivingLicenseCategoryEnum DrivingLicenseCategory { get; set; }
        public int NumberOfSeats { get; set; }
        public double PricePerDay { get; set; }
        public string RegistrationNumber { get; set; }
        public string VIN { get; set; }
        public CarStatusEnum Status { get; set; }
        public virtual List<Rental> Rentals { get; set; } = new List<Rental>();
#nullable enable
        public string? Comments { get; set; }
    }
}
