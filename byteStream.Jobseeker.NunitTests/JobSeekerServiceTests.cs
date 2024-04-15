using Microsoft.EntityFrameworkCore;
using NUnit.Framework.Legacy;
using NUnit.Framework;
using byteStream.JobSeeker.Api.Models;

using byteStream.JobSeeker.Api.Data;
using byteStream.JobSeeker.API.Services;
using System.Collections;
using FluentAssertions;

namespace byteStream.Jobseeker.NunitTests
{
    [TestFixture]
    public class JobSeekerServiceTests
    {
        private DbContextOptions<AppDbContext> options;

        private JobSeekers _user1;
        private JobSeekers _updatedUser1;
        private JobSeekers _user2;

        public JobSeekerServiceTests()
        {
            _user1 = new JobSeekers()
            {
                Id = new Guid("30D4466A-5127-46C1-8327-1BB9D4816434"),
                FirstName = "Mark",
                LastName = "Jhonson",
                Email = "markjhonson345@email.com",
                Phone = "8576940123",
                Address = "House No: 1002,\nRandom Street,\nRandom City",
                TotalExperience = 2.5,
                ExpectedSalary = 40000,
                DOB = new DateTime(1996, 06, 12),
                ResumeURL = "https://localhost:7004/Resumes/8edbd66b-3289-4535-90ee-77448716c03a.pdf",
                ProfileImgURL = "https://localhost:7004/Images/8edbd66b-3289-4535-90ee-77448716c03a.jpg",
                Qualification = null,
                      Experience = null
                     };

            _updatedUser1 = new()
            {
                Id = new Guid("30D4466A-5127-46C1-8327-1BB9D4816434"),
                FirstName = "Joey",
                LastName = "Tribiani",
                Email = "joetrib@outlook.com",
                Phone = "9865741230",
                Address = "House No: 1001,\nRandom Street,\nRandom City",
                TotalExperience = 7,
                ExpectedSalary = 80000,
                DOB = new DateTime(1998, 06, 12),
                ResumeURL = "https://localhost:7004/Resumes/8edbd66b-3289-4535-90ee-77448716c03a.pdf",
                ProfileImgURL = "https://localhost:7004/Images/8edbd66b-3289-4535-90ee-77448716c03a.jpg",
                Qualification =null,
                Experience = null,
            };

            _user2 = new JobSeekers()
            {
                Id = new Guid("3E5ECF05-83FD-4386-8B41-263D3DB4A7AF"),
                FirstName = "James",
                LastName = "Watson",
                Email = "jameswatson@gmail.com",
                Phone = "9587126340",
                Address = "House No: 1002,\nRandom Street,\nRandom City",
                TotalExperience = 5.5,
                ExpectedSalary = 50000,
                DOB = new DateTime(2001, 04, 20),
                ResumeURL = "https://localhost:7004/Resumes/8edbd66b-3289-4535-90ee-77448716c03a.pdf",
                ProfileImgURL = "https://localhost:7004/Images/8edbd66b-3289-4535-90ee-77448716c03a.jpg",
                Qualification = null,
                Experience = null,
            };
        }

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<AppDbContext>()
               .UseInMemoryDatabase(databaseName: "temp_JobSeeker").Options;
        }

        [Test]
        public async Task GetUsers_InputUserIdList_ReturnsUserListFromDatabase()
        {
            // Arrange
            using (var context = new AppDbContext(options))
            {
                context.Database.EnsureDeleted();
                await context.JobSeekerss.AddAsync(_user1);
                await context.JobSeekerss.AddAsync(_user2);
                await context.SaveChangesAsync();
            }
            List<Guid> userIds = new List<Guid> { new Guid("30D4466A-5127-46C1-8327-1BB9D4816434"), new Guid("3E5ECF05-83FD-4386-8B41-263D3DB4A7AF") };
            List<JobSeekers> expectedUserList = new List<JobSeekers> { _user1, _user2 };

            // Act 
            List<JobSeekers> userListFromDb;
            using (var context = new AppDbContext(options))
            {
                var repository = new JobSeekerService(context);
                userListFromDb = await repository.GetUsersAsync(userIds);
            }

            // Assert
            CollectionAssert.AreEqual(expectedUserList, userListFromDb, new UserCompare());
        }

        [Test]
        public async Task GetByUserIdAsync_UserDoesNotExists_ReturnsNull()
        {
            // Arrange
            using (var context = new AppDbContext(options))
            {
                context.Database.EnsureDeleted();
            }
            Guid userId = new Guid("30D4466A-5127-46C1-8327-1BB9D4816434");

            // Act 
            JobSeekers userFromDb;
            using (var context = new AppDbContext(options))
            {
                var repository = new JobSeekerService(context);
                userFromDb = await repository.GetByIdAsync(userId);
            }

            // Assert
            Assert.That(userFromDb, Is.Null);
        }

        [Test]
        public async Task CreateAsync_AddUser1_CheckUserInDatabase()
        {
            // Arrange
            using (var context = new AppDbContext(options))
            {
                context.Database.EnsureDeleted();
            }

            // Act
            using (var context = new AppDbContext(options))
            {
                var repository = new JobSeekerService(context);
                await repository.CreateAsync(_user1);
            }

            // Assert
            using (var context = new AppDbContext(options))
            {
                var userFromDb = context.JobSeekerss.FirstOrDefault(u => u.Id == new Guid("30D4466A-5127-46C1-8327-1BB9D4816434"));
                userFromDb.Should().BeEquivalentTo(_user1);
            }
        }

        [Test]
        public async Task UpdateAsync_User11_CheckUpdatedValuesFromDatabase()
        {
            // Arrange
            using (var context = new AppDbContext(options))
            {
                context.Database.EnsureDeleted();
                await context.JobSeekerss.AddAsync(_user1);
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new AppDbContext(options))
            {
                var repository = new JobSeekerService(context);
                await repository.UpdateAsync(_updatedUser1);
            }

            //assert
            using (var context = new AppDbContext(options))
            {
                var vacancyFromDb = context.JobSeekerss.FirstOrDefault(u => u.Id == new Guid("30D4466A-5127-46C1-8327-1BB9D4816434"));
                vacancyFromDb.Should().BeEquivalentTo(_updatedUser1);
                vacancyFromDb.Should().NotBeEquivalentTo(_user1);
            }
        }

        [Test]
        public async Task UpdateAsync_JobSeekerDoesNotExists_ReturnNull()
        {
            // Arrange
            using (var context = new AppDbContext(options))
            {
                context.Database.EnsureDeleted();
            }

            // Act
            JobSeekers result;
            using (var context = new AppDbContext(options))
            {
              
                var repository = new JobSeekerService(context);
                result = await repository.UpdateAsync(_updatedUser1);
            }

            //assert
            Assert.That(result, Is.Null);
        }

        private class UserCompare : IComparer
        {
            public int Compare(object x, object y)
            {
                var user1 = (JobSeekers)x;
                var user2 = (JobSeekers)y;
                if (user1.Id != user2.Id)
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