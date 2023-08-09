using CarRentalManagerAPI.Entities;
using CarRentalManagerAPI.Models.Rental;
using FluentValidation;
using System;
using System.Globalization;
using System.Linq;

namespace CarRentalManagerAPI.Models.Validators
{
    public class CreateRentalDtoValidator : AbstractValidator<CreateRentalDto>
    {
        public CreateRentalDtoValidator(CarRentalManagerDbContext dbContext)
        {
            RuleFor(p => p.ClientId)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    var client = dbContext.Clients.FirstOrDefault(c => c.Id == value);

                    if(client is null)
                    {
                        context.AddFailure("ClientId", "There is no client with the given id in the database");
                    }

                    if (client.IsBlocked)
                    {
                        context.AddFailure("ClientId", "Client is blocked");
                    }
                });

            RuleFor(p => p.UserId)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    var user = dbContext.Users.Any(u => u.Id == value);

                    if (!user)
                    {
                        context.AddFailure("UserId", "There is no user with the given id in the database");
                    }
                });

            RuleFor(p => p.CarId)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    var car = dbContext.Cars.FirstOrDefault(c => c.Id == value);

                    if (car is null)
                    {
                        context.AddFailure("CarId", "There is no car with the given id in the database");
                    }

                    if (car.Status != Enums.CarStatusEnum.Avaliable)
                    {
                        context.AddFailure("CarId", $"Car status is \"{car.Status.ToString()}\"");
                    }
                });

            RuleFor(p => p.RentalDate)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    var isValidDateFormat = DateTime.TryParseExact(value, "yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);

                    if (!isValidDateFormat)
                    {
                        context.AddFailure("RentalDate", "Invalid date format, required date format is ISO8601");
                    }
                });

            RuleFor(p => p.ExpectedDateOfReturn)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    var isValidDateFormat = DateTime.TryParseExact(value, "yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);

                    if (!isValidDateFormat)
                    {
                        context.AddFailure("ExpectedDateOfReturn", "Invalid date format, required date format is ISO8601 (\"yyyy-MM-ddTHH:mm:ss.fffZ\")");
                    }
                });
        }
    }
}
