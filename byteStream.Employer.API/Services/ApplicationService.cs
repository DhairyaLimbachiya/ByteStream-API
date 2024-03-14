using byteStream.Employer.Api.Models;
using byteStream.Employer.API.Data;
using byteStream.Employer.API.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace byteStream.Employer.API.Services
{
  
    public class ApplicationService : IApplicationService
    {
        private readonly AppDbContext _db;

        public ApplicationService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<UserVacancyRequests> CreateAsync(UserVacancyRequests request)
        {
            await _db.UserVacancyRequests.AddAsync(request);
            await _db.SaveChangesAsync();

            return request;
        }

        public async Task<List<UserVacancyRequests>> GetAllByUserIdAsync(Guid userId)
        {
            var result = await _db.UserVacancyRequests.Where(request => request.UserId == userId).Include(u => u.Vacancy).ToListAsync();
            return result;
        }

        public async Task<List<UserVacancyRequests>> GetAllByVacancyIdAsync(Guid vacancyId)
        {
            var result = await _db.UserVacancyRequests.Where(request => request.VacancyId == vacancyId).Include(u => u.Vacancy).ToListAsync();
            return result;
        }

        public async Task<UserVacancyRequests?> GetDetailAsync(Guid userId, Guid vacancyId)
        {
            return await _db.UserVacancyRequests.FirstOrDefaultAsync(u => u.VacancyId == vacancyId && u.UserId == userId);
        }
    }
}