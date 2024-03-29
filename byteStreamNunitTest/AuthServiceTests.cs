using AutoMapper;
using byteStream.Auth.Api.Controllers;
using byteStream.Auth.Api.Models;
using byteStream.Auth.Api.Services;
using byteStream.Auth.Api.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace byteStream.Auth.Api.Tests
{
    [TestFixture]
    public class AuthControllerTests
    {
        private AuthController _authController;
        private Mock<UserManager<ApplicationUser>> _userManagerMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IAuthService> _authServiceMock;

        [SetUp]
        public void Setup()
        {
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);

            _mapperMock = new Mock<IMapper>();
            _authServiceMock = new Mock<IAuthService>();

            _authController = new AuthController(_userManagerMock.Object, _mapperMock.Object, _authServiceMock.Object);
        }

        [Test]
        public async Task Register_ValidRequest_ReturnsOkResultWithResponseDto()
        {
            // Arrange
            var registerRequestDto = new RegisterRequestDto
            {
                FullName = "example@example.com",
                Password = "Password123",
                UserType = "Employer" // or "JobSeeker"
            };
            var responseDto = new ResponseDto { IsSuccess = true }; // Mock successful registration
            _authServiceMock.Setup(m => m.RegisterAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(responseDto);

            // Act
            var result = await _authController.Register(registerRequestDto) as OkObjectResult;

            // Assert
            Assert.That(result,Is.Not.Null);
            Assert.That(200,Is.EqualTo( result.StatusCode)); // Ok status code
          
            var response = result.Value as ResponseDto;
            Assert.That(response,Is.Not.Null);
            Assert.That(response.IsSuccess,Is.True);
            
        }

        [Test]
        public async Task Register_FailedRegistration_ReturnsBadRequestResultWithErrorMessage()
        {
            // Arrange
            var registerRequestDto = new RegisterRequestDto
            {
                FullName = "example@example.com",
                Password = "Password123",
                UserType = "Employer" // or "JobSeeker"
            };
            var errorMessage = "Failed to register user"; // Mock error message
            var responseDto = new ResponseDto { IsSuccess = false, Message = errorMessage }; // Mock failed registration
            _authServiceMock.Setup(m => m.RegisterAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(responseDto);

            // Act
            var result = await _authController.Register(registerRequestDto) as BadRequestObjectResult;

            // Assert
          
            Assert.That(400,Is.EqualTo( result.StatusCode)); // Bad request status code
            Assert.That(errorMessage,Is.EqualTo( result.Value)); // Check response message
        }


        [Test]
        public async Task Login_ValidLogin_ReturnsOkResultWithToken()
        {
            // Arrange
            var loginRequestDto = new LoginRequestDto();
            var jwtToken = "jwtTokenString";
            _authServiceMock.Setup(m => m.LoginAsync(loginRequestDto)).ReturnsAsync(jwtToken);

            // Act
            var result = await _authController.Login(loginRequestDto) as OkObjectResult;

            // Assert
           
            Assert.That(result, Is.Not.Null);
            var response = result.Value as LoginResponseDto;
            Assert.That(response,Is.Not.Null);
            Assert.That(jwtToken, Is.EqualTo(response.Token));

        }

        [Test]
        public async Task Login_InvalidLogin_ReturnsUnauthorizedResult()
        {
            // Arrange
            var loginRequestDto = new LoginRequestDto { UserName = "invalid@example.com", Password = "invalidPassword" };
            _authServiceMock.Setup(m => m.LoginAsync(loginRequestDto)).ReturnsAsync((string)null); // Mock invalid login

            // Act
            var result = await _authController.Login(loginRequestDto) as ObjectResult;

            // Assert
            Assert.That (result,Is.Not.Null);
            Assert.That(401, Is.EqualTo( result.StatusCode)); // Unauthorized status code
            Assert.That("Username or Password Incorrect!!!",Is.EqualTo( result.Value)); // Check response message
        }
        [Test]
        public async Task ForgotPassword_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var loginRequestDto = new LoginRequestDto { UserName = "test@example.com", Password = "newpassword" };
            var user = new ApplicationUser { Email = "test@example.com" };
            _userManagerMock.Setup(m => m.FindByEmailAsync(loginRequestDto.UserName)).ReturnsAsync(user);
            _userManagerMock.Setup(m => m.GeneratePasswordResetTokenAsync(user)).ReturnsAsync("resetToken");
            _userManagerMock.Setup(m => m.ResetPasswordAsync(user, "resetToken", loginRequestDto.Password)).ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _authController.ForgotPassword(loginRequestDto) as OkResult;

            // Assert
            Assert.That(result,Is.Not.Null);
            Assert.That(200, Is.EqualTo(result.StatusCode));
        }
        [Test]
        public async Task ForgotPassword_InvalidEmail_ReturnsNotFound()
        {
            // Arrange
            var request = new LoginRequestDto { UserName = "nonexistent@example.com", Password = "newPassword123" };
            _userManagerMock.Setup(m => m.FindByEmailAsync(request.UserName)).ReturnsAsync((ApplicationUser)null); // Mock user not found

            // Act
            var result = await _authController.ForgotPassword(request) as NotFoundResult;

            // Assert
            Assert.That(result,Is.Not.Null);
            Assert.That(404,Is.EqualTo( result.StatusCode)); // Not found status code
        }
    }
}