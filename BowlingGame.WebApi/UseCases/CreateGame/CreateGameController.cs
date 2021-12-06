using BowlingGame.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace BowlingGame.WebApi.UseCases.CreateGame
{
    [ApiController]
    [Route("api/v1/games")]
    [Produces("application/json")]
    [AllowAnonymous]
    public class CreateGameController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public CreateGameController(IMediator mediator, ILogger<CreateGameController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateGame([FromBody] CreateGameRequestModel request)
        {
            try
            {
                var createGameRequest = request.ToCreateGameRequestApplication();
                var createGameResponse = await _mediator.Send(createGameRequest);

                return Ok(createGameResponse.GameId);
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
