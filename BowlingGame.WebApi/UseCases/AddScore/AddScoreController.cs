using BowlingGame.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace BowlingGame.WebApi.UseCases.AddScore
{
    [ApiController]
    [Route("api/v1/games")]
    [Produces("application/json")]
    [AllowAnonymous]
    public class AddScoreController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public AddScoreController(IMediator mediator, ILogger<AddScoreController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPut]
        [Route("{gameId}/score")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddScoreResponseModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddScore(Guid gameId, [FromBody] AddScoreRequestModel request)
        {
            try
            {
                var addScoreRequest = request.ToAddScoreRequestApplication(gameId);
                var addScoreResponse = await _mediator.Send(addScoreRequest);

                return Ok(new AddScoreResponseModel
                {
                    Score = addScoreResponse.TotalPins
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
