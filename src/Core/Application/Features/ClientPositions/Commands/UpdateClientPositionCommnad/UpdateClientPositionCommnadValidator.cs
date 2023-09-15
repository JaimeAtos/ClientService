using FluentValidation;

namespace Application.Features.ClientPositions.Commands.UpdateClientPositionCommnad
{
    public class UpdateClientPositionCommnadValidator : AbstractValidator<UpdateClientPositionCommand>
    {
        public UpdateClientPositionCommnadValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
            RuleFor(c => c.ClientId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
            RuleFor(c => c.PositionId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
            RuleFor(c => c.PositionDescription)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(80).WithMessage("{PropertyName} must not exceed 80 characters.");
            RuleFor(c => c.CurrentStateId)
                .NotNull()
                .WithMessage("{PropertyName} is required.");
            RuleFor(c => c.CurrentStateName)
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(40).WithMessage("{PropertyName} must not exceed 40 characters.");
        }
    }
}