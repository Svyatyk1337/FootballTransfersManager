using FluentValidation;
using FootballTransfers.Application.DTOs;
using System;

namespace FootballTransfers.Application.Validators
{
    public class CreatePlayerDtoValidator : AbstractValidator<CreatePlayerDto>
    {
        public CreatePlayerDtoValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.DateOfBirth)
                .Must(d => d < DateTime.Today.AddYears(-16))
                .WithMessage("Player must be at least 16 years old");
            RuleFor(x => x.Nationality).MaximumLength(50);
            RuleFor(x => x.Height).InclusiveBetween(150, 220);
            RuleFor(x => x.Weight).InclusiveBetween(50, 120);
            RuleFor(x => x.MarketValue).GreaterThanOrEqualTo(0);
        }
    }
}
