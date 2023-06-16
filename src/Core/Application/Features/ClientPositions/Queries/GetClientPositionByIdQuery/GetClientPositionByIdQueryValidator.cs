using FluentValidation;

namespace Application.Features.ClientPositions.Queries.GetClientPositionByIdQuery
{
    public class GetClientPositionByIdQueryValidator : AbstractValidator<GetClientPositionByIdQuery>
    {
        public GetClientPositionByIdQueryValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
