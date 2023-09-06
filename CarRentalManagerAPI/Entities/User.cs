using System.Collections.Generic;

namespace CarRentalManagerAPI.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public virtual List<Rental> Rentals { get; set; } = new List<Rental>();
    }
}
