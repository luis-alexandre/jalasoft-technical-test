using BowlingGame.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace BowlingGame.WebApi.UseCases.GetScore
{
    [ApiController]
    [Route("api/v1/games")]
    [Produces("application/json")]
    [AllowAnonymous]
    public class GetScoreController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public GetScoreController(IMediator mediator, ILogger<GetScoreController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Route("{gameId}/score")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetScoreResponseModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetScore(Guid gameId)
        {
            try
            {
                var getScoreRequest = new Application.UseCases.GetScore.GetScoreRequest
                {
                    GameId = gameId
                };
                var getScoreResponse = await _mediator.Send(getScoreRequest);

                return Ok(new GetScoreResponseModel
                {
                    PlayerName = getScoreResponse.PlayerName,
                    Score = getScoreResponse.Score
                });
            }
            catch (GameException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
