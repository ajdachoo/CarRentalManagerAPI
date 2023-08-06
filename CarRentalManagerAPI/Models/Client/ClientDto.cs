using CarRentalManagerAPI.Entities;
using CarRentalManagerAPI.Enums;
using System.Collections.Generic;

namespace CarRentalManagerAPI.Models.Client
{
    public class ClientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PESELOrPassportNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsBlocked { get; set; }
        public List<string> DrivingLicenseCategories { get; set; }
        public string Comments { get; set; }
    }
}
