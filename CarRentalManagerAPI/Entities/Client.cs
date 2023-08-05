using CarRentalManagerAPI.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace CarRentalManagerAPI.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PESELOrPassportNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsBlocked { get; set; }
        public string DrivingLicenseCategories { get; set; }
        public virtual List<Rental> Rentals { get; set; } = new List<Rental>();
#nullable enable
        public string? Comments { get; set; }
    }
}
