﻿using FluentValidation;

namespace Application.Features.Client.Queries.GetClientById
{
    public class GetClientByIdValidator : AbstractValidator<GetClientByIdQuery>
    {
        public GetClientByIdValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
