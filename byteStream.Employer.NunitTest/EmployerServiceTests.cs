using byteStream.Employer.Api.Services;
using byteStream.Employer.API.Data;

using FluentAssertions;

using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace JobHunt.Services.EmployerAPI.Tests
{
    [TestFixture]
    public class EmployerServiceTests
    {
        private DbContextOptions<AppDbContext> options;

        private byteStream.Employer.Api.Models.Employeer _employer1;
        private byteStream.Employer.Api.Models.Employeer _updatedEmployer1;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<AppDbContext>()
               .UseInMemoryDatabase(databaseName: "temp_Employer").Options;
        }

        public EmployerServiceTests()
        {
            _employer1 = new byteStream.Employer.Api.Models.Employeer
            {
                Id = new Guid("59878AA6-BB5C-4A90-5A69-08DC39204FE5"),
                Organization = "Green Solutions",
                OrganizationType = "Agriculture",
                CompanyEmail = "info@greensolutions.com",
                CompanyPhone = "9988776655",
                NoOfEmployees = 100,
                StartYear = 2020,
                About = "Random Description",
                CreatedBy = "demoemployer@email.com",
                ProfileImageUrl = "https://localhost:7284/Images/2d967414-5688-4dc0-a23a-dbf7880b19d6.jpg",
            };

            _updatedEmployer1 = new byteStream.Employer.Api.Models.Employeer
            {
                Id = new Guid("59878AA6-BB5C-4A90-5A69-08DC39204FE5"),
                Organization = "Medi Solutions",
                OrganizationType = "Healthcare",
                CompanyEmail = "info@medisolutions.com",
                CompanyPhone = "9988776655",
                NoOfEmployees = 50,
                StartYear = 2020,
                About = "Random Description",
                CreatedBy = "demoemployer@email.com",
                ProfileImageUrl = "https://localhost:7284/Images/2d967414-5688-4dc0-a23a-dbf7880b19d6.jpg",
            };
        }

        [Test]
        public async Task GetByNameAsync_OrganizationDoesNotExists_ReturnsNull()
        {
            // Arrange
            string name = "Medi Solutions";

            // Act
            byteStream.Employer.Api.Models.Employeer result;
            using (var context = new AppDbContext(options))
            {
                var repository = new EmployerService(context);
                result = await repository.GetByCompanyName(name);
            }

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetByNameAsync_OrganizationExists_ReturnsEmployerFromDatabase()
        {
            // Arrange
            string name = "Green Solutions";
            using (var context = new AppDbContext(options))
            {
                context.Database.EnsureDeleted();
                await context.Employers.AddAsync(_employer1);
                await context.SaveChangesAsync();
            }

            // Act
            byteStream.Employer.Api.Models.Employeer result;
            using (var context = new AppDbContext(options))
            {
                var repository = new EmployerService(context);
                result = await repository.GetByCompanyName(name);
            }

            // Assert
            Assert.That(result, Is.Not.Null);
            result.Should().BeEquivalentTo(_employer1);
        }

      
        

        [Test]
        public async Task CreateAsync_AddEmployer1_CheckTheValuesFromDatabase()
        {
            // Arrange
            using (var context = new AppDbContext(options))
            {
                context.Database.EnsureDeleted();
            }

            // Act
            using (var context = new AppDbContext(options))
            {
                var repository = new EmployerService(context);
                await repository.CreateAsync(_employer1);
            }

            // Assert
            using (var context = new AppDbContext(options))
            {
                var vacancyFromDb = context.Employers.FirstOrDefault(u => u.Id == new Guid("59878AA6-BB5C-4A90-5A69-08DC39204FE5"));
                vacancyFromDb.Should().BeEquivalentTo(_employer1);
            }
        }

        [Test]
        public async Task UpdateAsync_OrganizationDoesNotExists_ReturnsNull()
        {
            // Arrange
            byteStream.Employer.Api.Models.Employeer result;
            using (var context = new AppDbContext(options))
            {
                context.Database.EnsureDeleted();
            }

            // Act
            using (var context = new AppDbContext(options))
            {
                var repository = new EmployerService(context);
                result = await repository.UpdateAsync(_updatedEmployer1);
            }

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task UpdateAsync_OrganizationExists_ReturnsUpdatedEmployerFromDatabase()
        {
            // Arrange
            using (var context = new AppDbContext(options))
            {
                context.Database.EnsureDeleted();
                await context.Employers.AddAsync(_employer1);
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new AppDbContext(options))
            {
                var repository = new EmployerService(context);
                await repository.UpdateAsync(_updatedEmployer1);
            }

            // Assert
            using (var context = new AppDbContext(options))
            {
                var vacancyFromDb = context.Employers.FirstOrDefault(u => u.Id == new Guid("59878AA6-BB5C-4A90-5A69-08DC39204FE5"));
                vacancyFromDb.Should().BeEquivalentTo(_updatedEmployer1);
            }
        }
    }
}