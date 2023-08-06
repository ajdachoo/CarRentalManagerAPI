using CarRentalManagerAPI.Entities;
using CarRentalManagerAPI.Enums;
using CarRentalManagerAPI.Models.Client;
using FluentValidation;
using System;
using System.Linq;

namespace CarRentalManagerAPI.Models.Validators
{
    public class CreateClientDtoValidator : AbstractValidator<CreateClientDto>
    {
        public CreateClientDtoValidator(CarRentalManagerDbContext dbContext)
        {
            RuleFor(p => p.IsBlocked)
                .NotEmpty();

            RuleFor(p => p.DrivingLicenseCategories)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    var isInCorrect = value.Any(c => !Enum.IsDefined(typeof(DrivingLicenseCategoryEnum), c.ToUpper()));

                    if(isInCorrect)
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
                .EmailAddress()
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Clients.Any(c => c.Email == value);

                    if (emailInUse)
                    {
                        context.AddFailure("Email", "That email adress is taken");
                    }
                });

            RuleFor(p => p.Name)
                .NotEmpty()
                .MaximumLength(25);

            RuleFor(p => p.PESELOrPassportNumber)
                .NotEmpty()
                .MaximumLength(25)
                .Custom((value, context) =>
                {
                    var peselOrPassportNumberInUse = dbContext.Clients.Any(c => c.PESELOrPassportNumber == value);

                    if (peselOrPassportNumberInUse)
                    {
                        context.AddFailure("PESELOrPassportNumber", "That pesel/passport number is taken");
                    }
                });

            RuleFor(p => p.PhoneNumber)
                .NotEmpty()
                .MaximumLength(9)
                .Custom((value, context) =>
                {
                    var phoneNumberInUse = dbContext.Clients.Any(c => c.PhoneNumber == value);

                    if (phoneNumberInUse)
                    {
                        context.AddFailure("PhoneNumber", "That phone number is taken");
                    }
                });
        }
    }
}
