using AutoMapper;
using ByteStream.Employer.Api.Controllers;
using byteStream.Employer.API.Models.Dto;
using byteStream.Employer.API.Models;
using byteStream.Employer.API.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Security.Claims;

using NUnit.Framework.Legacy;

namespace byteStreamNunitTest
{
    public class VacancyControllerTests
    {

        private VacancyController _controller;
        private Mock<IVacancyService> _vacancyServiceMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IEmployerService> _employerServiceMock;

        private List<Vacancy> _vacancylist;
        private Vacancy _vacancy;
        private Vacancy _updatedVacancy;
        private AddVacancyDto _newVacancyRequest;
        private VacancyDto _updatedVacancyRequest;


        public VacancyControllerTests()
        {
            _vacancy = new Vacancy()
            {
                Id = new Guid("61edda4f-ebdd-42f9-ed13-08dc39205bd7"),
                PublishedBy = "Green Solutions",
                PublishedDate = new DateTime(2024, 03, 05),
                NoOfVacancies = 2,
                MinimumQualification = "BE in Civil",
                JobTitle = "Site Manager",
                JobDescription = "job description for site manager job",
                ExperienceRequired = "3 to 5 years",
                LastDate = new DateTime(2024, 03, 8),
                MinSalary = 50000,
                MaxSalary = 30000,
            };

            _newVacancyRequest = new AddVacancyDto()
            {
                NoOfVacancies = 2,
                MinimumQualification = "BE in Civil",
                JobTitle = "Site Manager",
                JobDescription = "job description for site manager job",
                ExperienceRequired = "3 to 5 years",
                LastDate = new DateTime(2024, 03, 8),
                MinSalary = 50000,
                MaxSalary = 30000,
            };

            _updatedVacancyRequest = new VacancyDto()
            {
                PublishedBy = "Green Solutions",
                PublishedDate = new DateTime(2024, 03, 05),
                NoOfVacancies = 5, // Changed
                MinimumQualification = "BE in Civil",
                JobTitle = "Site Manager",
                JobDescription = "job description for site manager job",
                ExperienceRequired = "3 to 8 years", // Changed
                LastDate = new DateTime(2024, 03, 8),
                MinSalary = 30000, // Changed
                MaxSalary = 50000, // Changed
            };

            _updatedVacancy = new Vacancy()
            {
                Id = new Guid("61edda4f-ebdd-42f9-ed13-08dc39205bd7"),
                PublishedBy = "Green Solutions",
                PublishedDate = new DateTime(2024, 03, 05),
                NoOfVacancies = 5,
                MinimumQualification = "BE in Civil",
                JobTitle = "Site Manager",
                JobDescription = "job description for site manager job",
                ExperienceRequired = "3 to 8 years",
                LastDate = new DateTime(2024, 03, 8),
                MinSalary = 30000,
                MaxSalary = 50000,
            };

            _vacancylist = new List<Vacancy>([])
            {
                new Vacancy()
                {
                    Id = new Guid("674da545-6231-476e-e018-08dc3c4d373a"),
                    PublishedBy= "Medi Solutions",
                    PublishedDate= new DateTime(2024, 03, 04),
                    NoOfVacancies= 2,
                    MinimumQualification= "B. Pharm",
                    JobTitle= "Quality Manager",
                    JobDescription= "job description for quality manager job",
                    ExperienceRequired= "3 to 5 years",
                    LastDate= new DateTime(2024, 03, 27),
                    MinSalary= 30000,
                    MaxSalary= 37000,
                },
                new Vacancy()
                {
                    Id = new Guid("3660948d-570c-488b-4458-08dc3ccc67b1"),
                    PublishedBy= "Green Solutions",
                    PublishedDate= new DateTime(2024, 03, 05),
                    NoOfVacancies= 5,
                    MinimumQualification= "MBA",
                    JobTitle= "Senior Customer Executive",
                    JobDescription= "job description for Senior Customer Executive job",
                    ExperienceRequired= "3 to 5 years",
                    LastDate= new DateTime(2024, 03, 10),
                    MinSalary= 25000,
                    MaxSalary= 30000,
                }
            };
        }
        [SetUp]
        public void Setup()
        {
            _vacancyServiceMock = new Mock<IVacancyService>();
            _mapperMock = new Mock<IMapper>();
            _employerServiceMock = new Mock<IEmployerService>();
            _controller = new VacancyController(_vacancyServiceMock.Object, _mapperMock.Object, _employerServiceMock.Object);
        }

        [Test]

        public async Task GetById_ValidId_ReturnsOkResult()
        {
          
         
            bool isApplied = false; // Set the initial value of isApplied

            _vacancyServiceMock.Setup(s => s.CheckApplicationAsync(It.IsAny<Guid>(), _vacancy.Id)).ReturnsAsync(isApplied); // Mock the CheckApplicationAsync method
            _vacancyServiceMock.Setup(s => s.GetByIdAsync(_vacancy.Id)).ReturnsAsync(_vacancy); // Mock the GetByIdAsync method
            _mapperMock.Setup(m => m.Map<VacancyDto>(_vacancy)).Returns(_updatedVacancyRequest); // Mock the mapping from Vacancy to VacancyDto

            // Simulate the HttpContext with a user identity
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                new Claim(ClaimTypes.NameIdentifier, "42bf5685-6be6-44f5-85b4-084f6dfcc9b5")
                    }))
                }
            };


            // Act
            var result = await _controller.GetById(_vacancy.Id);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(200, Is.EqualTo(okResult.StatusCode));
            Assert.That(_updatedVacancyRequest, Is.EqualTo(okResult.Value));
        }



        [Test]
        public async Task Create_ValidRequest_ReturnsCreatedAtActionResult()
        {
            // Arrange

            _mapperMock.Setup(m => m.Map<Vacancy>(_newVacancyRequest)).Returns(_vacancy);
            _vacancyServiceMock.Setup(s => s.CreateAsync(_vacancy)).ReturnsAsync(_vacancy);
            _mapperMock.Setup(m => m.Map<VacancyDto>(_vacancy)).Returns(_updatedVacancyRequest);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier,  "42bf5685-6be6-44f5-85b4-084f6dfcc9b5") // Replace "UserIdValue" with a valid user
                                                                                                      // 
                    }))
                }
            };

            // Act
            var result = await _controller.Create(_newVacancyRequest);

            // Assert
            var createdAtActionResult = result as CreatedAtActionResult;
            Assert.That(createdAtActionResult,Is.Not.Null);
            Assert.That(201, Is.EqualTo(createdAtActionResult.StatusCode));
            Assert.That(nameof(_controller.GetById),Is.EqualTo( createdAtActionResult.ActionName));
           
            Assert.That(_updatedVacancyRequest,Is.EqualTo( createdAtActionResult.Value));
        }

       

        [Test]
        public async Task Update_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            _mapperMock.Setup(m => m.Map<Vacancy>(_updatedVacancyRequest)).Returns(_vacancy);
            _vacancyServiceMock.Setup(s => s.UpdateAsync(_vacancy)).ReturnsAsync(_updatedVacancy);
            _mapperMock.Setup(m => m.Map<VacancyDto>(_updatedVacancy)).Returns(_updatedVacancyRequest);

            // Act
            var result = await _controller.Update(_updatedVacancyRequest);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult,Is.Not.Null);
            Assert.That(200,Is.EqualTo( okResult.StatusCode));
            Assert.That(_updatedVacancyRequest,Is.EqualTo( okResult.Value));
        }


        [Test]
        public async Task Update_InvalidData_ReturnsBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("Error", "Invalid update data");
            var invalidUpdateDto = new VacancyDto()
            {
                JobTitle = "Senior Customer Executive",
                JobDescription = "job description for Senior Customer Executive job",
                ExperienceRequired = "3 to 5 years",
                LastDate = new DateTime(2024, 03, 10),
                MinSalary = 25000,
                MaxSalary = 30000,
            };

            // Act
            var result = await _controller.Update(invalidUpdateDto);

            // Assert
            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task GetAllVacancies_ReturnsOkResultWithVacancyList()
        {
            // Arrange
  

            _vacancyServiceMock.Setup(m => m.GetAllAsync()).ReturnsAsync(_vacancylist);

            // Act
            var result = await _controller.GetAllVacancies();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult,Is.Not.Null);
            Assert.That(StatusCodes.Status200OK,Is.EqualTo( okResult.StatusCode));

            var actualVacancyList = okResult.Value as List<Vacancy>;
            Assert.That(actualVacancyList,Is.Not.Null);
           

            // Assert that each vacancy in the expected list exists in the actual list
            foreach (var expectedVacancy in _vacancylist)
            {
                Assert.That(actualVacancyList.Any(v => v.Id == expectedVacancy.Id && v.JobTitle == expectedVacancy.JobTitle),Is.True);
            }
        }

        public async Task GetAllVacancies_ReturnsEmptyList_WhenNoVacanciesExist()
        {
            // Arrange
            var expectedVacancyList = new List<Vacancy>();

            _vacancyServiceMock.Setup(m => m.GetAllAsync()).ReturnsAsync(expectedVacancyList);

            // Act
            var result = await _controller.GetAllVacancies();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult,Is.Not.Null);
            Assert.That(StatusCodes.Status200OK,Is.EqualTo( okResult.StatusCode));

            var actualVacancyList = okResult.Value as List<Vacancy>;
            Assert.That(actualVacancyList,Is.Not.Null);
            Assert.That(actualVacancyList,Is.Empty);
        }



        [Test]
        public async Task GetByCompany_ReturnsVacancies_ForEmployer()
        {
            // Arrange
            var employerId = Guid.NewGuid(); 


            _vacancyServiceMock.Setup(m => m.GetByCompanyAsync(employerId)).ReturnsAsync(_vacancylist);

           
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                new Claim(ClaimTypes.NameIdentifier, employerId.ToString())
                    }))
                }
            };

            // Act
            var result = await _controller.GetByCompany();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult,Is.Not.Null);
            Assert.That(StatusCodes.Status200OK, Is.EqualTo( okResult.StatusCode));

            var actualVacancies = okResult.Value as List<Vacancy>;
            Assert.That(actualVacancies,Is.Not.Null);
            CollectionAssert.AreEqual(_vacancylist, actualVacancies);
        }


        [Test]
        public async Task GetByCompany_ReturnsNoContent_WhenNoVacanciesExist()
        {
            // Arrange
            var employerId = Guid.NewGuid(); // Generate a random employer ID for testing

            // Mock the GetByCompanyAsync method of the IVacancyService to return an empty list
            _vacancyServiceMock.Setup(m => m.GetByCompanyAsync(employerId)).ReturnsAsync(new List<Vacancy>());

            // Simulate the HttpContext with a user identity containing the employer ID
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                new Claim(ClaimTypes.NameIdentifier, employerId.ToString())
                    }))
                }
            };

            // Act
            var result = await _controller.GetByCompany();

            // Assert
            var noContentResult = result as NoContentResult;
            Assert.That(noContentResult,Is.Not.Null);
            Assert.That(StatusCodes.Status204NoContent,Is.EqualTo( noContentResult.StatusCode));
        }
    }



}


