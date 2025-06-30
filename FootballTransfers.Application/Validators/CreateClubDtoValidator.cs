using FluentValidation;
using FootballTransfers.Application.DTOs;

namespace FootballTransfers.Application.Validators
{
    public class CreateClubDtoValidator : AbstractValidator<CreateClubDto>
    {
        public CreateClubDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Country).NotEmpty().MaximumLength(50);
            RuleFor(x => x.City).MaximumLength(50);
            RuleFor(x => x.Stadium).MaximumLength(100);
        }
    }
}