using FluentValidation;

namespace Application.Features.ClientPositionManager.Commands.UpdateClientPositionManagerCommand
{
    internal class UpdateClientPositionManagerCommandValidator : AbstractValidator<UpdateClientPositionManagerCommand>
    {
        public UpdateClientPositionManagerCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
            RuleFor(c => c.ClientPositionId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
            RuleFor(c => c.ResourceId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
            RuleFor(c => c.Resource)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(150).WithMessage("{PropertyName} must not exceed 80 characters.");
        }
    }
}