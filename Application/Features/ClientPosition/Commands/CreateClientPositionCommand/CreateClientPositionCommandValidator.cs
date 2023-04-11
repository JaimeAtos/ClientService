using FluentValidation;

namespace Application.Features.ClientPosition.Commands.CreateClientPositionCommand
{
    public class CreateClientPositionCommandValidator : AbstractValidator<CreateClientPositionCommand>
    {
        public CreateClientPositionCommandValidator()
        {
             RuleFor(c => c.ClientId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
            RuleFor(c => c.PositionId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
            RuleFor(c => c.PositionName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(80).WithMessage("{PropertyName} must not exceed 80 characters.");
            RuleFor(c => c.RomaId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(80).WithMessage("{PropertyName} must not exceed 80 characters.");
        }
    }
}
