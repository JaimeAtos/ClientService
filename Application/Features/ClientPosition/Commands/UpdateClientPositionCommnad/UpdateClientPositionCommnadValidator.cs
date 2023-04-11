using FluentValidation;

namespace Application.Features.ClientPosition.Commands.UpdateClientPositionCommnad
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
