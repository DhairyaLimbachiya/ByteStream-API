using byteStream.Auth.Api.Models;
using byteStream.Auth.Api.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace byteStream.Tests.Services
{
    [TestFixture]
    public class TokenServiceTests
    {
        private TokenService _tokenService;
        private Mock<IConfiguration> _configurationMock;

        [SetUp]
        public void Setup()
        {
            _configurationMock = new Mock<IConfiguration>();
            _tokenService = new TokenService(_configurationMock.Object);
        }
        [Test]
        public void CreateJWTToken_ValidUserAndRoles_ReturnsValidToken()
        {
            // Arrange
            var applicationUser1 = new ApplicationUser
            {
                Id = "42bf5685-6be6-44f5-85b4-084f6dfcc9b5",
                Email = "test@example.com",
                UserName = "testuser"
            };
            var applicationUser2 = new ApplicationUser
            {
                Id = "43bf5685-6be6-44f5-85b4-084f6dfcc9b5",
                Email = "test2@example.com",
                UserName = "testuser2"
            };
            var roles = new List<string> { "Role1", "Role2" };

            _configurationMock.Setup(c => c["ApiSettings:JwtOptions:Secret"]).Returns("6P@eCp9mVs$%b5K*3vHnRgUjXn2r5u8x");
            _configurationMock.Setup(c => c["ApiSettings:JwtOptions:Audience"]).Returns("your_audience");
            _configurationMock.Setup(c => c["ApiSettings:JwtOptions:Issuer"]).Returns("your_issuer");

            // Act
            var token1 = _tokenService.CreateJWTToken(applicationUser1, roles);
            var token2 = _tokenService.CreateJWTToken(applicationUser2 , roles);
            // Assert
            Assert.That(token1,Is.Not.Null);

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token1);

            var claims = jwtToken.Claims;
            Assert.That("test@example.com",Is.EqualTo( claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email)?.Value));
            Assert.That("42bf5685-6be6-44f5-85b4-084f6dfcc9b5", Is.EqualTo(claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value));
            Assert.That("testuser", Is.EqualTo(claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name)?.Value));
            Assert.That(token1,Is.Not.EqualTo(token2)); 
        }
    }
}