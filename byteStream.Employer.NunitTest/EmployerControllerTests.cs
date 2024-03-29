using AutoMapper;
using byteStream.Employer.Api.Models;
using byteStream.Employer.API.Models.Dto;
using byteStream.Employer.API.Services.IServices;
using ByteStream.Employer.Api.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace byteStream.Employer.Api.Tests
{
    [TestFixture]
    public class EmployerControllerTests
    {
        private EmployerController _employerController;
        private Mock<IEmployerService> _employerServiceMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IImageService> _imageServiceMock;

        [SetUp]
        public void Setup()
        {
            _employerServiceMock = new Mock<IEmployerService>();
            _mapperMock = new Mock<IMapper>();
            _imageServiceMock = new Mock<IImageService>();
            _employerController = new EmployerController(_employerServiceMock.Object, _mapperMock.Object, _imageServiceMock.Object);
        }



        [Test]
        public async Task GetByCompany_ValidCompanyName_ReturnsOkResult()
        {
            // Arrange
            var companyName = "Example Company";
            var employerDto = new EmployerDto { Organization = companyName };
            _employerServiceMock.Setup(m => m.GetByCompanyName(companyName)).ReturnsAsync(new Employeer());
            _mapperMock.Setup(m => m.Map<EmployerDto>(It.IsAny<Employeer>())).Returns(employerDto);

            // Act
            var result = await _employerController.GetByCompany(companyName);


            var okResult = result as OkObjectResult;
            Assert.That(200, Is.EqualTo(okResult.StatusCode));
            Assert.That(employerDto, Is.EqualTo(okResult.Value));
        }

        [Test]
        public async Task GetByCompany_NullCompanyName_ReturnsBadRequestResult()
        {
            // Act
            var result = await _employerController.GetByCompany(null);


            var badRequestResult = result as BadRequestResult;
            Assert.That(400, Is.EqualTo(badRequestResult.StatusCode));
        }

        [Test]
        public async Task UploadImage_ValidRequest_ReturnsOkResultWithUrl()
        {
            // Arrange
            var fileMock = new Mock<IFormFile>();
            var fileName = "test.jpg";
            fileMock.Setup(f => f.FileName).Returns(fileName);
            fileMock.Setup(f => f.Length).Returns(1024); // Set any necessary properties for testing

            var fileExtension = ".jpg";
            var imageUrl = "https://example.com/image.jpg";

            var companyLogoDto = new CompanyLogoDto
            {
                FileExtension = fileExtension,
                FileName = fileName,
                Url = imageUrl
            };

            _imageServiceMock.Setup(s => s.Upload(It.IsAny<IFormFile>(), It.IsAny<CompanyLogoDto>())).ReturnsAsync(companyLogoDto);

            // Act
            var result = await _employerController.UploadImage(fileMock.Object, fileName);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult,Is.Not.Null);
            Assert.That(StatusCodes.Status200OK,Is.EqualTo( okResult.StatusCode));

            var responseDto = okResult.Value as ResponseDto;
            Assert.That(responseDto,Is.Not.Null);
            Assert.That(responseDto.IsSuccess,Is.True);
            Assert.That(imageUrl,Is.EqualTo( responseDto.Result));


        }
    }
}