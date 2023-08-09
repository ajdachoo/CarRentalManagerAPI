using CarRentalManagerAPI.Enums;
using System;

namespace CarRentalManagerAPI.Models.Rental
{
    public class CreateRentalDto
    {
        public int CarId { get; set; }
        public int ClientId { get; set; }
        public int UserId { get; set; }
        public string RentalDate { get; set; }
        public string ExpectedDateOfReturn { get; set; }
        public string Comments { get; set; }
    }
}
