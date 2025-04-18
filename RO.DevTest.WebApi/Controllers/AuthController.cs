using MediatR;
using Microsoft.AspNetCore.Mvc;
using RO.DevTest.Application.Features.Auth.Commands.LoginCommand;
using NSwag.Annotations;
using RO.DevTest.Domain.Exception;

namespace RO.DevTest.WebApi.Controllers
{
    [Route("api/auth")]
    [OpenApiTags("Auth")]
    public class AuthController(IMediator mediator) : Controller
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand? command)
        {
            if (command == null)
            {
                return BadRequest("Invalid login request.");
            }

            try
            {
                var response = await mediator.Send(command);
                
                return Ok(response);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}