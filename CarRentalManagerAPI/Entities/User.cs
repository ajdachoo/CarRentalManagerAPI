using System.Collections.Generic;

namespace CarRentalManagerAPI.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
#nullable enable
        public virtual List<Rental>? Rentals { get; set; }
    }
}
