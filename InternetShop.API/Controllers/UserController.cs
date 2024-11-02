using InternetShop.Application.Users.Commands;
using Microsoft.AspNetCore.Mvc;

namespace InternetShop.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser(
        RegisterUserHandler handler,
        [FromForm] RegisterUserCommand command)
        {
            var result = await handler.Handle(command);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> LoginUser(
        LoginUserHandler handler,
        [FromForm] LoginUserCommand command)
        {
            var result = await handler.Handle(command);

            if (result.IsFailure)
                return BadRequest(result.Error);

            HttpContext.Response.Cookies.Append("tasty-cookies", result.Value);

            return Ok();
        }
    }
}
