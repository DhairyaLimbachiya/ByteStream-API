using Microsoft.EntityFrameworkCore;
using NUnit.Framework.Legacy;
using NUnit.Framework;
using System;
using System.Collections;

using byteStream.JobSeeker.Api.Models;
using byteStream.JobSeeker.Api.Data;
using byteStream.JobSeeker.API.Services;
using FluentAssertions;


namespace byteStream.Jobseeker.NunitTests
{
    [TestFixture]
    public class ExperienceServiceTests
    {
        private DbContextOptions<AppDbContext> options;

        private Experience _experience1;
        private Experience _updatedExperience1;
        private Experience _experience2;

        public ExperienceServiceTests()
        {
            _experience1 = new Experience()
            {
                Id = new Guid("F32A812A-4190-4F00-3A8F-08DC3E97F46E"),
                UserID = new Guid("8EDBD66B-3289-4535-90EE-77448716C03A"),
                CompanyName = "Earth Solution",
                StartYear = new DateTime(2023, 05, 31),
                EndYear = new DateTime(2024, 02, 28),
                CompanyUrl = "www.earthsolutions.com",
                Designation = "Site Manager",
                JobDescription = "Random Description for the job",
            };

            _experience2 = new Experience()
            {
                Id = new Guid("00D6EE71-9751-42AD-4201-08DC4A4939FD"),
                UserID = new Guid("8EDBD66B-3289-4535-90EE-77448716C03A"),
                CompanyName = "Leaf Village Ninja School",
                StartYear = new DateTime(2019, 12, 31),
                EndYear = new DateTime(2022, 12, 30),
                CompanyUrl = "www.leafvillageschool.com",
                Designation = "Teacher",
                JobDescription = "Random Description for the Teacher Job",
            };

            _updatedExperience1 = new Experience()
            {
                Id = new Guid("F32A812A-4190-4F00-3A8F-08DC3E97F46E"),
                UserID = new Guid("8EDBD66B-3289-4535-90EE-77448716C03A"),
                CompanyName = "Construction Group",
                StartYear = new DateTime(2022, 05, 31),
                EndYear = new DateTime(2023, 02, 28),
                CompanyUrl = "www.constructiongroup.com",
                Designation = "Senior Site Manager",
                JobDescription = "Random Description for the senior job",
            };
        }

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<AppDbContext>()
               .UseInMemoryDatabase(databaseName: "temp_JobSeeker").Options;
        }

        [Test]
        public async Task GetAllByUserIDAsync_Experience1And2_ReturnsExperienceListFromDatabase()
        {
            // Arrange
            using (var context = new AppDbContext(options))
            {
                context.Database.EnsureDeleted();
                await context.Experiences.AddAsync(_experience1);
                await context.AddAsync(_experience2);
                await context.SaveChangesAsync();
            }
            Guid UserID = new Guid("8EDBD66B-3289-4535-90EE-77448716C03A");
            List<Experience> Experiences = new List<Experience>() { _experience1, _experience2 };

            // Act 
            List<Experience> ExperienceFromDb;
            using (var context = new AppDbContext(options))
            {
                var repository = new ExperienceService(context);
                ExperienceFromDb = await repository.GetAllAsync(UserID);
            }

            // Assert
            CollectionAssert.AreEqual(Experiences, ExperienceFromDb, new ExperienceCompare());
        }

        [Test]
        public async Task GetByIdAsync_ExperienceExists_ReturnExperienceFromDatabase()
        {
            // Arrange
            using (var context = new AppDbContext(options))
            {
                context.Database.EnsureDeleted();
                await context.Experiences.AddAsync(_experience1);
                await context.SaveChangesAsync();
            }
            Guid Id = new Guid("F32A812A-4190-4F00-3A8F-08DC3E97F46E");

            // Act 
            Experience ExperienceFromDb;
            using (var context = new AppDbContext(options))
            {
                var repository = new ExperienceService(context);
                ExperienceFromDb = await repository.GetByIdAsync(Id);
            }

            // Assert
            ExperienceFromDb.Should().BeEquivalentTo(_experience1);
        }

        [Test]
        public async Task GetByUserIDAsync_UserDoesNotExists_ReturnsNull()
        {
            // Arrange
            using (var context = new AppDbContext(options))
            {
                context.Database.EnsureDeleted();
            }
            Guid Id = new Guid("8A74A371-5BD2-484D-D653-08DC3E924DC5");

            // Act 
            Experience ExperienceFromDb;
            using (var context = new AppDbContext(options))
            {
                var repository = new ExperienceService(context);
                ExperienceFromDb = await repository.GetByIdAsync(Id);
            }

            // Assert
            Assert.That(ExperienceFromDb, Is.Null);
        }

        [Test]
        public async Task CreateAsync_AddExperience_CheckThatValueFromDatabase()
        {
            // Arrange
            using (var context = new AppDbContext(options))
            {
                context.Database.EnsureDeleted();
            }

            // Act
            using (var context = new AppDbContext(options))
            {
                var repository = new ExperienceService(context);
                await repository.CreateAsync(_experience1);
            }

            // Assert
            using (var context = new AppDbContext(options))
            {
                var userFromDb = context.Experiences.FirstOrDefault(u => u.Id == new Guid("F32A812A-4190-4F00-3A8F-08DC3E97F46E"));
                userFromDb.Should().BeEquivalentTo(_experience1);
            }
        }

        [Test]
        public async Task UpdateAsync_Experience1Exists_CheckUpdatedValuesFromDatabase()
        {
            // Arrange
            using (var context = new AppDbContext(options))
            {
                context.Database.EnsureDeleted();
                await context.Experiences.AddAsync(_experience1);
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new AppDbContext(options))
            {
                var repository = new ExperienceService(context);
                await repository.UpdateAsync(_updatedExperience1);
            }

            //assert
            using (var context = new AppDbContext(options))
            {
                var ExperienceFromDb = context.Experiences.FirstOrDefault(u => u.Id == new Guid("F32A812A-4190-4F00-3A8F-08DC3E97F46E"));
                ExperienceFromDb.Should().BeEquivalentTo(_updatedExperience1);
                ExperienceFromDb.Should().NotBeEquivalentTo(_experience1);
            }
        }

        [Test]
        public async Task UpdateAsync_ExperienceDoesNotExists_ReturnNull()
        {
            // Arrange
            using (var context = new AppDbContext(options))
            {
                context.Database.EnsureDeleted();
            }

            // Act
            Experience result;
            using (var context = new AppDbContext(options))
            {
                var repository = new ExperienceService(context);
                result = await repository.UpdateAsync(_updatedExperience1);
            }

            //assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task DeleteAsync_DeleteExperience1_CheckExperienceDeletedFromDatabase()
        {
            // Arrange
            using (var context = new AppDbContext(options))
            {
                context.Database.EnsureDeleted();
                await context.Experiences.AddAsync(_experience1);
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new AppDbContext(options))
            {
                var repository = new ExperienceService(context);
                await repository.DeleteAsync(_experience1.Id);
            }

            //assert
            using (var context = new AppDbContext(options))
            {
                var experienceFromDb = context.Experiences.FirstOrDefault(u => u.Id == new Guid("F32A812A-4190-4F00-3A8F-08DC3E97F46E"));
                Assert.That(experienceFromDb, Is.Null);
            }
        }

        private class ExperienceCompare : IComparer
        {
            public int Compare(object x, object y)
            {
                var Experience1 = (Experience)x;
                var Experience2 = (Experience)y;
                if (Experience1.Id != Experience2.Id)
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