using BowlingGame.Application.UseCases.CreateGame;
using System.ComponentModel.DataAnnotations;

namespace BowlingGame.WebApi.UseCases.CreateGame
{
    public class CreateGameRequestModel
    {
        [Required]
        public string PlayerName { get; set; }

        public CreateGameRequest ToCreateGameRequestApplication()
        {
            return new CreateGameRequest
            {
                PlayerName = PlayerName,
            };
        }
    }
}
