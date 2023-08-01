using CarRentalManagerAPI.Enums;

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
        public int PricePerDay { get; set; }
        public string RegistrationNumber { get; set; }
        public string VIN { get; set; }
        public RentalStatusEnum Status { get; set; }
    }
}
