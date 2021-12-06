using FluentValidation;
using System;

namespace BowlingGame.Application.UseCases.AddScore
{
    public class AddScoreRequestValidator : AbstractValidator<AddScoreRequest>
    {
        public AddScoreRequestValidator()
        {
            RuleFor(x => x.GameId)
                .NotNull()
                .NotEqual(Guid.Empty)
                .WithMessage("The Game ID must be provided.");

            RuleFor(x => x.NumberPins)
                .Must(x => x >= 0 && x <= 10)
                .WithMessage("The Number of Pins must be greater than or equal to 0 or less than equal to 10.");
        }
    }
}
