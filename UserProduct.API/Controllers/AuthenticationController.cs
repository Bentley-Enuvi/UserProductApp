using Microsoft.AspNetCore.Mvc;
using UserProduct.API.Response;
using UserProduct.Core.Abstractions;
using UserProduct.Core.DTOs;

namespace UserProduct.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        private readonly IEmailSenderService _emailSenderService;

        public AuthenticationController(IAuthenticationService authService, IEmailSenderService emailSenderService)
        {
            _authService = authService;
            _emailSenderService = emailSenderService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegRequestDto registerUserDto)
        {
            var result = await _authService.Register(registerUserDto);

            if (result.IsFailure)
                return BadRequest(ResponseDto<object>.Failure(result.Errors));

            // Send confirmation email
            // Send confirmation email
            var message = new Message(
                "Registration Confirmation",
                new List<string> { registerUserDto.Email },
                "Thank you for registering. Your account has been successfully created."
            );

            //  var emailSent = await _messengerService.Send(message);

            //if (!emailSent)
            //    return StatusCode(500, ResponseDto<object>.Failure(new List<Error> { new("Email.Error", "Failed to send confirmation email.") }));

            return Ok(ResponseDto<object>.Success("user registered succesfully"));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginUserDto)
        {
            var result = await _authService.Login(loginUserDto);

            if (result.IsFailure)
                return BadRequest(ResponseDto<object>.Failure(result.Errors));

            return Ok(ResponseDto<object>.Success(result.Data));
        }

    }
}
