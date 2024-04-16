using Azure;
using byteStream.Employer.Api.Models;
using byteStream.Employer.API.Data;
using byteStream.Employer.API.Models.Dto;
using byteStream.Employer.API.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace byteStream.Employer.API.Services
{
  
    public class ApplicationService : IApplicationService
    {
        private readonly AppDbContext db;

        public ApplicationService(AppDbContext db)
        {
           this.db = db;
        }

        /// <summary>
        /// This service is used to add Job Application in the user vacancy request table
        /// </summary>
        /// <returns></returns>
        public async Task<UserVacancyRequests> CreateAsync(UserVacancyRequests request)
        {
            await db.UserVacancyRequests.AddAsync(request);
            await db.SaveChangesAsync();

            return request;
        }
        /// <summary>
        /// It gets List data of all the users through the user ID
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserVacancyRequests>> GetAllByUserIdAsync(Guid userId)
        {
            var result = await db.UserVacancyRequests.Where(request => request.UserId == userId).Include(u => u.Vacancy).ToListAsync();
            return result;
        }
        /// <summary>
        /// It gets data of all the vacancies through vacancy Id
        /// </summary>
        /// <returns></returns>

        public async Task<List<UserVacancyRequests>> GetAllByVacancyIdAsync(Guid vacancyId)
        {
            var result = await db.UserVacancyRequests.Where(request => request.VacancyId == vacancyId).Include(u => u.Vacancy).ToListAsync();
            return result;
        }
        /// <summary>
        /// It gets data of all the vacancies through vacancy Id using Strored procedure
        /// </summary>
        /// <returns></returns>

        public async Task<List<UserVacancyRequests>> GetAllVacnacyByPageAsync(SP_VacancyRequestDto request)
        {
            var result = db.UserVacancyRequests.FromSql($"UserVacancyRequestsSP @vacancyId = {request.VacancyId}, @pageNumber = {request.PageNumber}, @pageSize = {request.PageSize}").ToList();
            List<UserVacancyRequests> response = result;
            foreach (var item in response)
            {
                item.Vacancy = await db.Vacancies.FirstOrDefaultAsync(u => u.Id == item.VacancyId);
            }

            return response;
        }

        /// <summary>
        ///     It gets the Details of a Job application through vacancy id and UserId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="vacancyId"></param>
        /// <returns></returns>
        public async Task<UserVacancyRequests?> GetDetailAsync(Guid userId, Guid vacancyId)
        {
            return await db.UserVacancyRequests.FirstOrDefaultAsync(u => u.VacancyId == vacancyId && u.UserId == userId);
        }
        /// <summary>
        ///         ///     It gets the Details of a Job application through its Id

        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserVacancyRequests?> GetDetailByIdAsync(Guid id)
        {
            return await db.UserVacancyRequests.FirstOrDefaultAsync(u=>u.Id == id);
        }

        /// <summary>
        /// It is used to update the Application status and reduce the count of vacancy by 1 whenever any application is accepted
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserVacancyRequests?> UpdateAsync(UserVacancyRequests request)
        {
            var existingApplication = await db.UserVacancyRequests.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (existingApplication != null)
            {
                var currentvacancy = db.Vacancies.FirstOrDefault(x => x.Id == request.VacancyId);

                if (request.ApplicationStatus == "Accepted" && currentvacancy.NoOfVacancies>0)
                {
                    currentvacancy.NoOfVacancies = currentvacancy.NoOfVacancies - 1;
                }

                db.Entry(existingApplication).CurrentValues.SetValues(request);
                await db.SaveChangesAsync();
                return request;
            }
            return null;
        }
        
    }
    }
