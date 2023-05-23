using System.Data;
using FluentValidation;

namespace Application.Features.ClientPositionsManager.Commands.DeleteClientPositionManagerCommand;

public class DeleteClientPositionManagerCommandValidator : AbstractValidator<DeleteClientPositionManagerCommand>
{
    public DeleteClientPositionManagerCommandValidator()
    {
        RuleFor(r => r.Id)
            .NotEmpty()
            .WithMessage("{PropertyName} must not be empty");
    }
}