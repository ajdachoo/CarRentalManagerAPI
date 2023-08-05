using CarRentalManagerAPI.Entities;
using CarRentalManagerAPI.Enums;
using CarRentalManagerAPI.Models.Car;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CarRentalManagerAPI.Models.Validators
{
    public class UpdateCarDtoValidator : AbstractValidator<UpdateCarDto>
    {
        public UpdateCarDtoValidator(CarRentalManagerDbContext dbContext)
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
                .MaximumLength(10);

            RuleFor(p => p.VIN)
                .NotEmpty()
                .MaximumLength(25);

            RuleFor(p => p.Status)
                .NotEmpty()
                .IsEnumName(typeof(CarStatusEnum), false);

            RuleFor(p => p.Comments)
                .MaximumLength(50);
        }
    }
}
