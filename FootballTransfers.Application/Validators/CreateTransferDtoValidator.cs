using FluentValidation;
using FootballTransfers.Application.DTOs;
using System;

public class CreateTransferDtoValidator : AbstractValidator<CreateTransferDto>
{
    public CreateTransferDtoValidator()
    {
        RuleFor(x => x.PlayerId).GreaterThan(0);
        RuleFor(x => x.ToClubId).GreaterThan(0);
        RuleFor(x => x.TransferFee).GreaterThanOrEqualTo(0);
        RuleFor(x => x.TransferDate).LessThanOrEqualTo(DateTime.Now.AddMonths(1));
        RuleFor(x => x.ContractLength).MaximumLength(20);
    }
}