
using byteStream.Employer.API.Models.Dto;
using byteStream.Employer.API.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;


namespace byteStream.Employer.NunitTest
{
    [TestFixture]
    public class ImageServiceTests
    {
        private ImageService _ImageService;
        private Mock<IWebHostEnvironment> _webHostEnvironmentMock;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
      
        [SetUp]
        public void Setup()
        {
            _webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
            _webHostEnvironmentMock.Setup(m => m.ContentRootPath).Returns("D:\\ByteStream\\API\\byteStream.Employer.NunitTest");
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            _ImageService = new ImageService(_webHostEnvironmentMock.Object, _httpContextAccessorMock.Object);
        }

        [Test]
        public async Task Upload_WithValidFile_ShouldReturnCompanyLogoDTO()
        {
            // Arrange
            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(f => f.CopyToAsync(It.IsAny<Stream>(), default)).Returns(Task.CompletedTask);

            var companyLogoDTO = new CompanyLogoDto
            {
                FileName = "test",
                FileExtension = ".png"
            };

            var httpRequestMock = new DefaultHttpContext().Request;
            httpRequestMock.Scheme = "http";
            httpRequestMock.Host = new HostString("example.com");
            httpRequestMock.PathBase = new PathString("/basepath");

            _httpContextAccessorMock.Setup(m => m.HttpContext.Request).Returns(httpRequestMock);

            // Act
            var result = await _ImageService.Upload(fileMock.Object, companyLogoDTO);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Url, Is.EqualTo("http://example.com/basepath/Images/test.png"));
        }
    }
}
