using AutoMapper;
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
		private readonly IMapper mapper;
		private readonly IAuthService authService;
		protected ResponseDto _responseDto;

		public AuthController(IMapper mapper, IAuthService authService)
		{
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
			return (Ok("Username or Password Incorrect!!!"));


		}
		[Authorize(Roles ="Employer")]
		[HttpGet]

		public async Task<IActionResult> hello()
		{
			return Ok("hello");
		}

	}
}
