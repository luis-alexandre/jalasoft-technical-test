using BowlingGame.Application.UseCases.AddScore;
using System;
using System.ComponentModel.DataAnnotations;

namespace BowlingGame.WebApi.UseCases.AddScore
{
    public class AddScoreRequestModel
    {
        [Required]
        public int NumberPins { get; set; }

        public AddScoreRequest ToAddScoreRequestApplication(Guid gameId)
        {
            return new AddScoreRequest
            {
                GameId = gameId,
                NumberPins = NumberPins
            };
        }
    }
}
