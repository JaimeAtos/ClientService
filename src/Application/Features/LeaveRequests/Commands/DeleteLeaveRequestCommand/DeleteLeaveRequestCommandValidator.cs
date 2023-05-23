using FluentValidation;

namespace Application.Features.LeaveRequests.Commands.DeleteLeaveRequestCommand;

public class DeleteLeaveRequestCommandValidator : AbstractValidator<DeleteLeaveRequestCommand>
{
    public DeleteLeaveRequestCommandValidator()
    {
        RuleFor(r => r.Id)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
    }
}