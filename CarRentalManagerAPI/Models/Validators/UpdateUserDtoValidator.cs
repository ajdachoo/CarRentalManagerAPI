using CarRentalManagerAPI.Entities;
using CarRentalManagerAPI.Models.User;
using FluentValidation;
using System.Linq;

namespace CarRentalManagerAPI.Models.Validators
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator(CarRentalManagerDbContext dbContext)
        {
            RuleFor(p => p.PhoneNumber)
                .NotNull()
                .MaximumLength(9);

            RuleFor(p => p.Surname)
                .NotNull()
                .MaximumLength(25);

            RuleFor(p => p.Name)
                .NotNull()
                .MaximumLength(25);
        }
    }
}
