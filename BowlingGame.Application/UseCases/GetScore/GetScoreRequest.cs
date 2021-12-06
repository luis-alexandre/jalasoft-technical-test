using MediatR;
using System;

namespace BowlingGame.Application.UseCases.GetScore
{
    public class GetScoreRequest : IRequest<GetScoreResponse>
    {
        public Guid GameId { get; set; }
    }
}
