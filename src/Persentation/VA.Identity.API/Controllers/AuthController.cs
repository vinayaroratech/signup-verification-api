using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using VA.Identity.Application.Features.Users.Commands;
using VA.Identity.Application.Jwt;

namespace VA.Identity.API.Controllers
{
    [Route("api/account")]
    public class AuthController : MainController
    {
        private readonly AppJwtSettings _appJwtSettings;

        public AuthController(
            IOptions<AppJwtSettings> appJwtSettings)
        {
            _appJwtSettings = appJwtSettings.Value;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterUserCommand command)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            var result = await Mediator.Send(command);

            if (result.Succeeded)
            {
                return CustomResponse(result.Data);
            }

            foreach (string error in result.Errors)
            {
                AddError(error);
            }

            return CustomResponse();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUserCommand command)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            var result = await Mediator.Send(command);

            if (result.Succeeded)
            {
                return CustomResponse(result.Data);
            }

            foreach (string error in result.Errors)
            {
                AddError(error);
            }

            return CustomResponse();
        }
    }
}