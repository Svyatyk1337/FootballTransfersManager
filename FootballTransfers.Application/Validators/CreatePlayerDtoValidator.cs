using FluentValidation;
using FootballTransfers.Application.DTOs;
using System;

public class CreatePlayerDtoValidator : AbstractValidator<CreatePlayerDto>
{
    public CreatePlayerDtoValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.DateOfBirth).LessThan(DateTime.Now.AddYears(-16));
        RuleFor(x => x.Nationality).MaximumLength(50);
        RuleFor(x => x.Height).InclusiveBetween(150, 220);
        RuleFor(x => x.Weight).InclusiveBetween(50, 120);
        RuleFor(x => x.MarketValue).GreaterThanOrEqualTo(0);
    }
}