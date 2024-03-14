using byteStream.Employer.Api.Models;
using byteStream.Employer.API.Data;
using byteStream.Employer.API.Models;
using byteStream.Employer.API.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace ByteStream.Employer.Api.Repository
{
	public class VacancyService : IVacancyService
	{
		private readonly AppDbContext dbContext;

		public VacancyService(AppDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

        public async Task<Vacancy?> GetByIdAsync(Guid id)
        {
            return await dbContext.Vacancies.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Vacancy> CreateAsync(Vacancy vacancy)
		{
			await dbContext.Vacancies.AddAsync(vacancy);
			await dbContext.SaveChangesAsync();
			return vacancy;
		}

		public async Task<Vacancy?> DeleteAsync(Guid id)
		{
			var existing = await dbContext.Vacancies.FirstOrDefaultAsync(x => x.Id == id);
			if (existing == null) return null;
			dbContext.Vacancies.Remove(existing);
			await dbContext.SaveChangesAsync();
			return existing;
		}

        public async Task<List<Vacancy?>> GetByCompanyAsync(Guid id)
        {
            var user = await dbContext.Employers.FirstOrDefaultAsync(x => x.Id == id);
			var organizationName = user.Organization;
			var vacancylist= await dbContext.Vacancies.Where(x=>x.PublishedBy==organizationName).ToListAsync();
			return vacancylist;
				}

        public async Task<Vacancy?> UpdateAsync(Vacancy vacancy)
		{
			dbContext.Vacancies.Update(vacancy);
		
			await dbContext.SaveChangesAsync();
			return vacancy;
		}

        public async Task<List<Vacancy?>> GetAllAsync()
        {
			var vacancylist=await dbContext.Vacancies.ToListAsync();
			return vacancylist;
        }

	

        public async Task<UserVacancyRequests> CreateUserVacancyAsync(UserVacancyRequests userVacancyRequests)
        {
            await dbContext.UserVacancyRequests.AddAsync(userVacancyRequests);
            await dbContext.SaveChangesAsync();
            return userVacancyRequests;

        }

        public  async Task<bool> CheckApplicationAsync(Guid userId, Guid vacancyId)
        {
            var result = await dbContext.UserVacancyRequests.FirstOrDefaultAsync(u => u.VacancyId == vacancyId && u.UserId == userId);
            if (result != null)
            {
                return true;
            }
            return false;
        }
    }
    }
	

