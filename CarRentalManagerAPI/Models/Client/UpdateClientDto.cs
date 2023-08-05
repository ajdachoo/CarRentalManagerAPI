namespace CarRentalManagerAPI.Models.Client
{
    public class UpdateClientDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PESELOrPassportNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsBlocked { get; set; }
        public string DrivingLicenseCategories { get; set; }
        public string Comments { get; set; }
    }
}
