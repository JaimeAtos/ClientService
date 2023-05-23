using FluentValidation;

namespace Application.Features.ClientPositionsManager.Commands.CreateClientPositionManager
{
    internal class CreateClientPositionManagerValidator : AbstractValidator<CreateClientPositionManagerCommand>
    {
        public CreateClientPositionManagerValidator()
        {
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