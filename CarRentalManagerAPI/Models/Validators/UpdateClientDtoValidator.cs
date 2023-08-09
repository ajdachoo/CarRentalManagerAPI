using CarRentalManagerAPI.Entities;
using CarRentalManagerAPI.Enums;
using CarRentalManagerAPI.Models.Client;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CarRentalManagerAPI.Models.Validators
{
    public class UpdateClientDtoValidator : AbstractValidator<UpdateClientDto>
    {
        public UpdateClientDtoValidator(CarRentalManagerDbContext dbContext)
        {
            RuleFor(p => p.IsBlocked)
                .NotNull();

            RuleFor(p => p.DrivingLicenseCategories)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    var isInCorrect = value.Any(c => !Enum.IsDefined(typeof(DrivingLicenseCategoryEnum), c.ToUpper()));

                    if (isInCorrect)
                    {
                        context.AddFailure("DrivingLicenseCategories", "Invalid data");
                    }
                });

            RuleFor(p => p.Comments)
                .MaximumLength(50);

            RuleFor(p => p.Surname)
                .NotEmpty()
                .MaximumLength(25);

            RuleFor(p => p.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(p => p.Name)
                .NotEmpty()
                .MaximumLength(25);

            RuleFor(p => p.PESELOrPassportNumber)
                .NotEmpty()
                .MaximumLength(25);

            RuleFor(p => p.PhoneNumber)
                .NotEmpty()
                .MaximumLength(9);
        }
    }
}
