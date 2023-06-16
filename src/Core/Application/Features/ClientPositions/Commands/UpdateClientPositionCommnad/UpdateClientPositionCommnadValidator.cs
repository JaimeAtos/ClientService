using FluentValidation;

namespace Application.Features.ClientPositions.Commands.UpdateClientPositionCommnad
{
    public class UpdateClientPositionCommnadValidator : AbstractValidator<UpdateClientPositionCommnad>
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
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
            RuleFor(c => c.CurrentStateName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(40).WithMessage("{PropertyName} must not exceed 80 characters.");
        }
    }
}