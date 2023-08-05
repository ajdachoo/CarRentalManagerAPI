using CarRentalManagerAPI.Entities;
using CarRentalManagerAPI.Enums;
using CarRentalManagerAPI.Models.Car;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CarRentalManagerAPI.Models.Validators
{
    public class CreateCarDtoValidator : AbstractValidator<CreateCarDto>
    {
        public CreateCarDtoValidator(CarRentalManagerDbContext dbContext)
        {
            RuleFor(p => p.Mark)
                .NotEmpty()
                .MaximumLength(25);

            RuleFor(p => p.Model)
                .NotEmpty()
                .MaximumLength(25);

            RuleFor(p => p.Transmission)
                .NotEmpty()
                .IsEnumName(typeof(TransmissionEnum), false);

            RuleFor(p => p.EnginePower)
                .NotEmpty()
                .InclusiveBetween(1, 10000);

            RuleFor(p => p.DrivingLicenseCategory)
                .NotEmpty()
                .IsEnumName(typeof(DrivingLicenseCategoryEnum), false);

            RuleFor(p => p.NumberOfSeats)
                .NotEmpty()
                .InclusiveBetween(1, 10);

            RuleFor(p => p.PricePerDay)
                .NotEmpty()
                .InclusiveBetween(1, 1000000);

            RuleFor(p => p.RegistrationNumber)
                .NotEmpty()
                .MaximumLength(10)
                .Custom((value, context) =>
                {
                    var registrationNumberInUse = dbContext.Cars.Any(c => c.RegistrationNumber == value);
                    if (registrationNumberInUse)
                    {
                        context.AddFailure("RegistrationNumber", "That registration number is taken");
                    }
                });

            RuleFor(p => p.VIN)
                .NotEmpty()
                .MaximumLength(25)
                .Custom((value, context) =>
                {
                    var vinInUse = dbContext.Cars.Any(c => c.VIN == value);
                    if (vinInUse)
                    {
                        context.AddFailure("VIN", "That VIN is taken");
                    }
                });

            RuleFor(p=>p.Status)
                .NotEmpty()
                .IsEnumName(typeof(CarStatusEnum), false);

            RuleFor(p => p.Comments)
                .MaximumLength(50);
        }
    }
}
