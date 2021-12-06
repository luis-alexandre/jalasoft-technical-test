using FluentValidation;

namespace BowlingGame.Application.UseCases.CreateGame
{
    public class CreateGameRequestValidator : AbstractValidator<CreateGameRequest>
    {
        public CreateGameRequestValidator()
        {
            RuleFor(r => r.PlayerName).NotEmpty()
                .WithMessage("The Player Name must be provided.");

            RuleFor(r => r.PlayerName)
                .Must(x => x.Length <= 100)
                .WithMessage("The Title must have a maximum of 100 characters.");
        }
    }
}
