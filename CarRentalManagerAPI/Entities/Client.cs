using CarRentalManagerAPI.Enums;

namespace CarRentalManagerAPI.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PESELOrPassportNumber { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public DrivingLicenseCategoryEnum DrivingLicenseCategory { get; set; }
        public bool IsBlocked { get; set; }
        #nullable enable
        public string? Comments { get; set; }

    }
}
