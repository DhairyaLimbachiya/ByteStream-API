using byteStream.JobSeeker.API.Models.Dto;
using byteStream.JobSeeker.API.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace byteStream.Jobseeker.NunitTests
{
   [TestFixture]

    public class UploadRepositoryTests

    {

        private UploadService _uploadService;

        private Mock<IWebHostEnvironment> _webHostEnvironmentMock;

        private Mock<IHttpContextAccessor> _httpContextAccessorMock;

       

        [SetUp]

        public void Setup()

        {

            _webHostEnvironmentMock = new Mock<IWebHostEnvironment>();

            _webHostEnvironmentMock.Setup(m => m.ContentRootPath).Returns("D:\\ByteStream\\API\\byteStream.Jobseeker.NunitTests\\StaticFiles\\");

            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            _uploadService = new UploadService(_webHostEnvironmentMock.Object, _httpContextAccessorMock.Object);

        }




        [Test]

        public async Task UploadResume_WithValidFile_ShouldReturnResumeUrl()

        {

            // Arrange

            var fileMock = new Mock<IFormFile>();

            fileMock.Setup(f => f.CopyToAsync(It.IsAny<Stream>(), default)).Returns(Task.CompletedTask);

            var uploadDto = new UploadDto

            {

                FileName = "testresume",

                FileExtension = ".pdf"

            };




            var httpRequestMock = new DefaultHttpContext().Request;

            httpRequestMock.Scheme = "http";

            httpRequestMock.Host = new HostString("example.com");

            httpRequestMock.PathBase = new PathString("/basepath");




            _httpContextAccessorMock.Setup(m => m.HttpContext.Request).Returns(httpRequestMock);




            // Act

            var result = await _uploadService.Upload(fileMock.Object, uploadDto);




            // Assert

            Assert.That(result, Is.Not.Null);

            Assert.That(result.Url, Is.EqualTo("http://example.com/basepath/Resumes/testresume.pdf"));

        }




        [Test]

        public async Task UploadImage_WithValidFile_ShouldReturnImageUrl()

        {

            // Arrange

            var fileMock = new Mock<IFormFile>();

            fileMock.Setup(f => f.CopyToAsync(It.IsAny<Stream>(), default)).Returns(Task.CompletedTask);




            var uploadDto = new UploadDto

            {

                FileName = "testimage",

                FileExtension = ".jpg"

            };




            var httpRequestMock = new DefaultHttpContext().Request;

            httpRequestMock.Scheme = "http";

            httpRequestMock.Host = new HostString("example.com");

            httpRequestMock.PathBase = new PathString("/basepath");




            _httpContextAccessorMock.Setup(m => m.HttpContext.Request).Returns(httpRequestMock);




            // Act

            var result = await _uploadService.UploadImage(fileMock.Object, uploadDto);




            // Assert

            Assert.That(result, Is.Not.Null);

            Assert.That(result.Url, Is.EqualTo("http://example.com/basepath/Images/testimage.jpg"));

        }

    }

}