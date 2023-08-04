using CarRentalManagerAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace CarRentalManagerAPI.Models.Car
{
    public class CreateCarDto
    {
        [Required]
        [MaxLength(25)]
        public string Mark { get; set; }
        [Required]
        [MaxLength(25)]
        public string Model { get; set; }
        [Required]
        public string Transmission { get; set; }
        [Required]
        [Range(1, 10000)]
        public int EnginePower { get; set; }
        [Required]
        public string DrivingLicenseCategory { get; set; }
        [Required]
        [Range(1, 10)]
        public int NumberOfSeats { get; set; }
        [Required]
        [Range(1, 1000000)]
        public double PricePerDay { get; set; }
        [Required]
        [MaxLength(10)]
        public string RegistrationNumber { get; set; }
        [Required]
        [MaxLength(25)]
        public string VIN { get; set; }
        [Required]
        public string Status { get; set; }
        [MaxLength(50)]
        public string Comments { get; set; }
    }
}
