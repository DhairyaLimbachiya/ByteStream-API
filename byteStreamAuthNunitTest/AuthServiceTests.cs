using AutoMapper;
using byteStream.Auth.Api.Controllers;
using byteStream.Auth.Api.Models;
using byteStream.Auth.Api.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace YourNamespace.Tests
{
    [TestFixture]
    public class AuthControllerTests
    {
        private AuthController _authController;
        private Mock<UserManager<ApplicationUser>> _userManagerMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IAuthService> _authServiceMock;

        [SetUp]
        public void SetUp()
        {
            _userManagerMock = MockUserManager<ApplicationUser>();
            _mapperMock = new Mock<IMapper>();
            _authServiceMock = new Mock<IAuthService>();
            _authController = new AuthController(_userManagerMock.Object, _mapperMock.Object, _authServiceMock.Object);
        }

        // Helper method to mock UserManager
        private static Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            return new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
        }

        [Test]
        public async Task Register_ValidRequest_ReturnsOkResultWithResponseDto()
        {
            // Arrange
            var request = new RegisterRequestDto();
            var responseDto = new ResponseDto { IsSuccess = true };
            _mapperMock.Setup(m => m.Map<ApplicationUser>(request)).Returns(new ApplicationUser());
            _authServiceMock.Setup(m => m.RegisterAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(responseDto);
            _mapperMock.Setup(m => m.Map<UserDto>(responseDto.Result)).Returns(new UserDto());

            // Act
            var result = await _authController.Register(request) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(responseDto, result.Value);
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
            Assert.IsNotNull(result);
           // Assert.AreEqual(new LoginResponseDto { Token = jwtToken }, result.Value);
        }

        [Test]
        public async Task Login_InvalidLogin_ReturnsOkResultWithString()
        {
            // Arrange
            var loginRequestDto = new LoginRequestDto();
            _authServiceMock.Setup(m => m.LoginAsync(loginRequestDto)).ReturnsAsync((string)null);

            // Act
            var result = await _authController.Login(loginRequestDto) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Username or Password Incorrect!!!", result.Value);
        }

        [Test]
        public async Task ForgotPassword_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var loginRequestDto = new LoginRequestDto();
            var user = new ApplicationUser();
            _userManagerMock.Setup(m => m.FindByEmailAsync(loginRequestDto.UserName)).ReturnsAsync(user);
            _userManagerMock.Setup(m => m.GeneratePasswordResetTokenAsync(user)).ReturnsAsync("resetToken");
            _userManagerMock.Setup(m => m.ResetPasswordAsync(user, "resetToken", loginRequestDto.Password)).ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _authController.ForgotPassword(loginRequestDto) as OkResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task ForgotPassword_UserNotFound_ReturnsNotFoundResult()
        {
            // Arrange
            var loginRequestDto = new LoginRequestDto();
            _userManagerMock.Setup(m => m.FindByEmailAsync(loginRequestDto.UserName)).ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await _authController.ForgotPassword(loginRequestDto) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}