using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Client.Commands.UpdateClientCommnad
{
    public class UpdateClientCommandValidator : AbstractValidator<UpdateClientCommand>
    {
        public UpdateClientCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(80).WithMessage("{PropertyName} must not exceed 80 characters.");
            RuleFor(c => c.LocationId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
            RuleFor(c => c.CountPositions)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");
           
        }
    }
    
    
}
