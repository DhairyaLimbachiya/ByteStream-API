using AutoMapper;
using Azure;
using byteStream.Auth.Api.Models;
using byteStream.Auth.Api.Services.IServices;
using byteStream.Auth.Api.Utility.ApiFilter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace byteStream.Auth.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper mapper;
		private readonly IAuthService authService;
		protected ResponseDto _responseDto;
 

        public AuthController(UserManager<ApplicationUser> userManager, IMapper mapper, IAuthService authService)
		{
            _userManager = userManager;
            this.mapper = mapper;
			this.authService = authService;
			_responseDto = new ResponseDto();
            
        }

   
        [HttpPost("register")]
		[ValidateModel]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var user = mapper.Map<ApplicationUser>(request);
            var roleName = request.UserType.ToLower() == "employer" ? "Employer" : "JobSeeker";
            _responseDto = await authService.RegisterAsync(user, request.Password, roleName);

            if (!_responseDto.IsSuccess)
            {
                return BadRequest(_responseDto.Message); 
            }

            _responseDto.Result = mapper.Map<UserDto>(_responseDto.Result);
            return Ok(_responseDto);
        }

        [HttpPost("login")]
		[ValidateModel]

		public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
		{
			var jwt = await authService.LoginAsync(loginRequestDto);
			if (jwt != null)
			{
				var response = new LoginResponseDto
				{
					Token = jwt
				};
				return Ok(response);
			}
			return (Unauthorized("Username or Password Incorrect!!!"));


		}

        [HttpPost]
        [Route("forgotpassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] LoginRequestDto request)
        {
            
            {
                var user = await _userManager.FindByEmailAsync(request.UserName);
                if (user == null)
                {
                    return NotFound();
                }
                else
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var identityResult = await _userManager.ResetPasswordAsync(user, token, request.Password);
                    
                }
            }
            return Ok();
        }


    }
}
