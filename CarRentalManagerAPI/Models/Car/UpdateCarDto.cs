using System.ComponentModel.DataAnnotations;

namespace CarRentalManagerAPI.Models.Car
{
    public class UpdateCarDto
    {
        public string Mark { get; set; }
        public string Model { get; set; }
        public string Transmission { get; set; }
        public int EnginePower { get; set; }
        public string DrivingLicenseCategory { get; set; }
        public int NumberOfSeats { get; set; }
        public double PricePerDay { get; set; }
        public string RegistrationNumber { get; set; }
        public string VIN { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
    }
}
