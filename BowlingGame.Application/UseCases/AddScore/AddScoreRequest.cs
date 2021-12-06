using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace BowlingGame.Application.UseCases.AddScore
{
    public class AddScoreRequest : IRequest<AddScoreResponse>
    {
        [Required]
        public Guid GameId { get; set; }

        [Required]
        public int NumberPins { get; set; }
    }
}
