using CarRentalManagerAPI.Enums;
using System;

namespace CarRentalManagerAPI.Entities
{
    public class Rental
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public virtual Car Car { get; set; }
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ExpectedDateOfReturn { get; set; }
        public RentalStatusEnum Status { get; set; }
        public double Amount { get; set; }

#nullable enable
        public DateTime? DateOfReturn { get; set; }
        public string? Comments { get; set; }

    }
}
