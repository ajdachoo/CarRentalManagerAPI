using CarRentalManagerAPI.Entities;
using CarRentalManagerAPI.Models.User;
using FluentValidation;
using System.Linq;

namespace CarRentalManagerAPI.Models.Validators
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator(CarRentalManagerDbContext dbContext)
        {
            RuleFor(p => p.PhoneNumber)
                .NotNull()
                .MaximumLength(9)
                .Custom((value, context) =>
                {
                    var phoneNumberInUse = dbContext.Users.Any(c => c.PhoneNumber == value);

                    if (phoneNumberInUse)
                    {
                        context.AddFailure("PhoneNumber", "That phone number is taken");
                    }
                });

            RuleFor(p => p.Surname)
                .NotNull()
                .MaximumLength(25);

            RuleFor(p => p.Name)
                .NotNull()
                .MaximumLength(25);
        }
    }
}
