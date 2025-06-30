using FluentValidation;
using FootballTransfers.Application.DTOs;

namespace FootballTransfers.Application.Validators
{
    public class CreateAgentDtoValidator : AbstractValidator<CreateAgentDto>
    {
        public CreateAgentDtoValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Email).EmailAddress().When(x => !string.IsNullOrEmpty(x.Email));
            RuleFor(x => x.Phone).MaximumLength(20);
        }
    }
}