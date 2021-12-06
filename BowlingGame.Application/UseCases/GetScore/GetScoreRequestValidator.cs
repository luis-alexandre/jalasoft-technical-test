using FluentValidation;
using System;

namespace BowlingGame.Application.UseCases.GetScore
{
    public class GetScoreRequestValidator : AbstractValidator<GetScoreRequest>
    {
        public GetScoreRequestValidator()
        {
            RuleFor(x => x.GameId)
                .NotNull()
                .NotEqual(Guid.Empty)
                .WithMessage("The Game ID must be provided.");
        }
    }
}
