using byteStream.Employer.API.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework.Legacy;
using NUnit.Framework;
using System;
using System.Collections;
using byteStream.Employer.API.Data;
using byteStream.Employer.API.Services;
using FluentAssertions;
using byteStream.Employer.API.Models.Dto;
using byteStream.Employer.Api.Models;

namespace byteStream.Employer.NunitTest
{
    [TestFixture]
    public class VacancyRepositoryTests
    {
        private DbContextOptions<AppDbContext> options;

        private Vacancy _vacancy1;
        private  Vacancy _vacancy2;
        private  Vacancy _updatedVacancy;
        public VacancyRepositoryTests()
        {
            _vacancy1 = new Vacancy()
            {
                Id = new Guid("674da545-6231-476e-e018-08dc3c4d373a"),
                PublishedBy = "Medi Solutions",
                PublishedDate = new DateTime(2024, 03, 04),
                NoOfVacancies = 2,
                MinimumQualification = "B. Pharm",
                JobTitle = "Quality Manager",
                JobDescription = "job description for quality manager job",
                ExperienceRequired = "3 to 5 years",
                LastDate = new DateTime(2024, 03, 27),
                MinSalary = 30000,
                MaxSalary = 37000,
            };

            _vacancy2 = new Vacancy()
            {
                Id = new Guid("3660948d-570c-488b-4458-08dc3ccc67b1"),
                PublishedBy = "Green Solutions",
                PublishedDate = new DateTime(2024, 03, 05),
                NoOfVacancies = 5,
                MinimumQualification = "MBA",
                JobTitle = "Senior Customer Executive",
                JobDescription = "job description for Senior Customer Executive job",
                ExperienceRequired = "3 to 5 years",
                LastDate = new DateTime(2024, 03, 10),
                MinSalary = 25000,
                MaxSalary = 30000,
            };

            _updatedVacancy = new Vacancy
            {
                Id = _vacancy1.Id,
                PublishedBy = _vacancy1.PublishedBy,
                PublishedDate = _vacancy1.PublishedDate,
                NoOfVacancies = 3, // Updated value
                MinimumQualification = _vacancy1.MinimumQualification,
                JobTitle = _vacancy1.JobTitle,
                JobDescription = _vacancy1.JobDescription,
                ExperienceRequired = _vacancy1.ExperienceRequired,
                LastDate = _vacancy1.LastDate,
                MinSalary = _vacancy1.MinSalary,
                MaxSalary = _vacancy1.MaxSalary
            };
        }

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<AppDbContext>()
               .UseInMemoryDatabase(databaseName: "temp_Employer").Options;
        }

        [Test]
        [Order(1)]
        public async Task CreateAsync_Vacancy1_CheckTheValuesFromDatabase()
        {
            // Act
            using (var context = new AppDbContext(options))
            {
                var repository = new VacancyService(context);
                await repository.CreateAsync(_vacancy1);
            }

            //assert
            using (var context = new AppDbContext(options))
            {
                var vacancyFromDb = context.Vacancies.FirstOrDefault(u => u.Id == new Guid("674da545-6231-476e-e018-08dc3c4d373a"));
                vacancyFromDb.Should().BeEquivalentTo(_vacancy1);
            }
        }

        [Test]
        [Order(2)]
        public async Task GetByIdAsync_Vacancy1_ChecktheVacancyFromDatabase()
        {
            // Act 
            Vacancy actualVacancy;
            using (var context = new AppDbContext(options))
            {
                var repository = new VacancyService(context);
                actualVacancy = await repository.GetByIdAsync(new Guid("674da545-6231-476e-e018-08dc3c4d373a"));
                actualVacancy.Should().BeEquivalentTo(_vacancy1);
            }
        }

        [Test]
        [Order(3)]
        public async Task GetAllAsync_Vacancy1AndVacancy2_CheckBoththeVacancyFromDatabase()
        {
            // Arrange
            var expecedList = new List<Vacancy> { _vacancy1, _vacancy2 };

            using (var context = new AppDbContext(options))
            {
                context.Database.EnsureDeleted();
                var repository = new VacancyService(context);
                await repository.CreateAsync(_vacancy1);
                await repository.CreateAsync(_vacancy2);
            }

            // Act
            List<Vacancy> actualList;
            using (var context = new AppDbContext(options))
            {
                var repository = new VacancyService(context);
                actualList = await repository.GetAllAsync();
            }

            //  Assert
            CollectionAssert.AreEqual(expecedList, actualList, new VacancyCompare());
        }


        [Test]
        [Order(4)]
        public async Task UpdateAsync_ValidVacancy_ReturnsUpdatedVacancy()
        {
            
            using (var context = new AppDbContext(options))
            {
                // Act
                var repository = new VacancyService(context);
                await repository.UpdateAsync(_updatedVacancy);

                // Assert
                var updatedVacancyFromDb = await context.Vacancies.FindAsync(_vacancy1.Id);
                updatedVacancyFromDb.Should().BeEquivalentTo(_updatedVacancy);
            }
        }


        [Test]
        public async Task DeleteAsync_ValidId_DeletesVacancy()
        {
            // Arrange
            var vacancyId = Guid.NewGuid(); // Assuming a valid vacancy ID
            using (var context = new AppDbContext(options))
            {
                context.Database.EnsureDeleted();
                await context.Vacancies.AddAsync(_vacancy1);
                await context.SaveChangesAsync();
            }

            // Act
            Vacancy result;
            using (var context = new AppDbContext(options))
            {
                var vacancyService = new VacancyService(context);
                result = await vacancyService.DeleteAsync(vacancyId);
            }

            // Assert
            using (var context = new AppDbContext(options))
            {
                var deletedVacancy = await context.Vacancies.FirstOrDefaultAsync(x => x.Id == vacancyId);

                deletedVacancy.Should().BeNull(); // Ensure that the vacancy is no longer present in the database
          
            }
        }

        [Test]
        public async Task DeleteAsync_NonExistingVacancy_ReturnsNull()
        {
            // Arrange
            var nonExistingId = Guid.NewGuid(); // Assuming a non-existing vacancy ID

            using (var context = new AppDbContext(options))
            {
                context.Database.EnsureDeleted();
                var repository = new VacancyService(context);
                // No vacancies added to the context
            }

            // Act
            Vacancy result;
            using (var context = new AppDbContext(options))
            {
                var vacancyService = new VacancyService(context);
                result = await vacancyService.DeleteAsync(nonExistingId);
            }

            // Assert
            Assert.That(result,Is.Null); // Ensure that null is returned when the vacancy does not exist
        }

        [Test]
        public async Task CheckApplicationAsync_Application1ExistsAndApplication2DoesNotExists_ReturnAppropriateResponse()
        {
            // Arrange
            var application1 = new UserVacancyRequests
            {
                Id = new Guid("1A3D8C7E-DB11-45FE-C2B6-08DC3C361C6E"),
                VacancyId = new Guid("61EDDA4F-EBDD-42F9-ED13-08DC39205BD7"),
                UserId = new Guid("DFB764C1-0DEA-45A0-B04F-E138C0E08C22"),
                ApplicationStatus = "Pending",
                AppliedDate = new DateTime(2024, 03, 04),
                TotalRecords = null,
            };
            using (var context = new AppDbContext(options))
            {
                context.Database.EnsureDeleted();
                await context.UserVacancyRequests.AddAsync(application1);
                await context.SaveChangesAsync();
            }

            // Act
            bool result1, result2;
            using (var context = new AppDbContext(options))
            {
                var repository = new VacancyService(context);
                result1 = await repository.CheckApplicationAsync(new Guid("DFB764C1-0DEA-45A0-B04F-E138C0E08C22"), new Guid("61EDDA4F-EBDD-42F9-ED13-08DC39205BD7"));
                result2 = await repository.CheckApplicationAsync(new Guid("8EDBD66B-3289-4535-90EE-77448716C03A"), new Guid("674DA545-6231-476E-E018-08DC3C4D373A"));
            }

            // Assert
            Assert.That(result1, Is.True);
            Assert.That(result2, Is.False);
        }

        private class VacancyCompare : IComparer
        {
            public int Compare(object x, object y)
            {
                var vacancy1 = (Vacancy)x;
                var vacancy2 = (Vacancy)y;
                if (vacancy1.Id != vacancy2.Id)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }

           
        }
    }
}