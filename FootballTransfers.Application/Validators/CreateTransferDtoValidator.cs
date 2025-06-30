using FluentValidation;
using FootballTransfers.Application.DTOs;
using System;

namespace FootballTransfers.Application.Validators
{
    public class CreateTransferDtoValidator : AbstractValidator<CreateTransferDto>
    {
        public CreateTransferDtoValidator()
        {
            RuleFor(x => x.PlayerId).GreaterThan(0);
            RuleFor(x => x.ToClubId).GreaterThan(0);
            RuleFor(x => x.TransferFee).GreaterThanOrEqualTo(0);
            RuleFor(x => x.TransferDate)
                .Must(date => date <= DateTime.Now.AddMonths(1))
                .WithMessage("TransferDate must not be later than one month from now");
            RuleFor(x => x.ContractLength).MaximumLength(20);
        }
    }
}
