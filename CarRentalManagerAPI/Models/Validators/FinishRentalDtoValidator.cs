using CarRentalManagerAPI.Entities;
using CarRentalManagerAPI.Models.Rental;
using FluentValidation;
using System.Globalization;
using System;

namespace CarRentalManagerAPI.Models.Validators
{
    public class FinishRentalDtoValidator : AbstractValidator<FinishRentalDto>
    {
        public FinishRentalDtoValidator(CarRentalManagerDbContext dbContext)
        {
            RuleFor(p => p.DateOfReturn)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    var isValidDateFormat = DateTime.TryParseExact(value, "yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);

                    if (!isValidDateFormat)
                    {
                        context.AddFailure("RentalDate", "Invalid date format, required date format is ISO8601");
                    }
                });
        }
    }
}
