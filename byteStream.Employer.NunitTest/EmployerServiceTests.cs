using byteStream.Employer.Api.Models;
using byteStream.Employer.Api.Services;
using byteStream.Employer.API.Data;

using FluentAssertions;

using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;

namespace byteStream.Employer.Api.Tests
{
    [TestFixture]
    public class EmployerServiceTests
    {
        private DbContextOptions<AppDbContext> options;

        private Employeer _employer1;
        private Employeer _updatedEmployer1;

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
                Organization = "Blue Solutions",
                OrganizationType = "Technology",
                CompanyEmail = "info@bluesolutions.com",
                CompanyPhone = "9988776655",
                NoOfEmployees = 100,
                StartYear = 2018,
                About = "Random Description",
                CreatedBy = "employer@example.com",
                ProfileImageUrl = "https://example.com/images/profile.jpg",
                
            };

            _updatedEmployer1 = new byteStream.Employer.Api.Models.Employeer
            {
                Id = new Guid("59878AA6-BB5C-4A90-5A69-08DC39204FE5"),
                Organization = "Red Solutions",
                OrganizationType = "Healthcare",
                CompanyEmail = "info@redsolutions.com",
                CompanyPhone = "9988776655",
                NoOfEmployees = 50,
                StartYear = 2018,
                About = "Random Description",
                CreatedBy = "employer@example.com",
                ProfileImageUrl = "https://example.com/images/profile.jpg",
            };
        }

        [Test]
        public async Task GetByNameAsync_OrganizationDoesNotExist_ReturnsNull()
        {
            // Arrange
            string name = "Nonexistent Solutions";

            // Act
              Employeer result;
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
            string name = "Blue Solutions";
            using (var context = new AppDbContext(options))
            {
                context.Database.EnsureDeleted();
                await context.Employers.AddAsync(_employer1);
                await context.SaveChangesAsync();
            }

            // Act
            Employeer result;
            using (var context = new AppDbContext(options))
            {
                var repository = new EmployerService(context);
                result = await repository.GetByCompanyName(name);
            }

            // Assert
            result.Should().NotBeNull();
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
                var employerFromDb = context.Employers.FirstOrDefault(u => u.Id == _employer1.Id);
                employerFromDb.Should().NotBeNull();
                employerFromDb.Should().BeEquivalentTo(_employer1);
            }
        }

        [Test]
        public async Task UpdateAsync_OrganizationDoesNotExist_ReturnsNull()
        {
            // Arrange
            Employeer result;
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
                var employerFromDb = context.Employers.FirstOrDefault(u => u.Id == _updatedEmployer1.Id);
                employerFromDb.Should().NotBeNull();
                employerFromDb.Should().BeEquivalentTo(_updatedEmployer1);
            }
        }

        [Test]
        public async Task GetByIdAsync_ExistingEmployerId_ReturnsEmployer()
        {
            // Arrange
            using (var context = new AppDbContext(options))
            {
                context.Database.EnsureDeleted();
                await context.Employers.AddAsync(_employer1);
                await context.SaveChangesAsync();
            }

            // Act
            Employeer result;
            using (var context = new AppDbContext(options))
            {
                var service = new EmployerService(context);
                result = await service.GetByIdAsync(_employer1.Id);
            }

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(_employer1);
        }

        [Test]
        public async Task GetByIdAsync_NonExistingEmployerId_ReturnsNull()
        {
            // Arrange
            var nonExistingId = Guid.NewGuid();

            // Act
            byteStream.Employer.Api.Models.Employeer result;
            using (var context = new AppDbContext(options))
            {
                var service = new EmployerService(context);
                result = await service.GetByIdAsync(nonExistingId);
            }

            // Assert
            result.Should().BeNull();
        }
    }
}