using FluentValidation;

namespace Application.Features.ClientPosition.Commands.DeleteClientPositionCommand
{
    public class DeleteClientPositionCommandValidator : AbstractValidator<DeleteClientPositionCommand>
    {
        public DeleteClientPositionCommandValidator()
        {
          RuleFor(c => c.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
