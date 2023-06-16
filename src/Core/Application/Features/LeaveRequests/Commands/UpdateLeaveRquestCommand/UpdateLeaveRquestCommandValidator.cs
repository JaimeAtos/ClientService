﻿using FluentValidation;

namespace Application.Features.LeaveRequests.Commands.UpdateLeaveRquestCommand
{
    internal class UpdateLeaveRquestCommandValidator : AbstractValidator<UpdateLeaveRequestCommand>
    {
        public UpdateLeaveRquestCommandValidator()
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
            RuleFor(c => c.ReasonId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
            RuleFor(c => c.LeaveReasonComments)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(150).WithMessage("{PropertyName} must not exceed 120 characters.");
        }
    }
}