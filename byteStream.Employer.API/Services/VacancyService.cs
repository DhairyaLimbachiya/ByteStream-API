using byteStream.Employer.Api.Models;
using byteStream.Employer.API.Data;
using byteStream.Employer.API.Models;
using byteStream.Employer.API.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace byteStream.Employer.API.Services
{
	public class VacancyService : IVacancyService
	{
		private readonly AppDbContext dbContext;

		public VacancyService(AppDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		/// <summary>
		/// To get the Details of a particular vacancy 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
        public async Task<Vacancy?> GetByIdAsync(Guid id)
        {
            return await dbContext.Vacancies.FirstOrDefaultAsync(x => x.Id == id);
        }
		/// <summary>
		/// To add new vacancy to the DataBase
		/// </summary>
		/// <param name="vacancy"></param>
		/// <returns></returns>
        public async Task<Vacancy> CreateAsync(Vacancy vacancy)
		{
			await dbContext.Vacancies.AddAsync(vacancy);
			await dbContext.SaveChangesAsync();
			return vacancy;
		}

		/// <summary>
		/// To Delete the vacancy from the database using the vacancy Id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task<Vacancy?> DeleteAsync(Guid id)
		{
			var existing = await dbContext.Vacancies.FirstOrDefaultAsync(x => x.Id == id);
			if (existing == null) return null;
			dbContext.Vacancies.Remove(existing);
			await dbContext.SaveChangesAsync();
			return existing;
		}
		
		/// <summary>
		/// to get the List of vacancies of any particular vacancy
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
        public async Task<List<Vacancy?>> GetByCompanyAsync(Guid id)
        {
            var user = await dbContext.Employers.FirstOrDefaultAsync(x => x.Id == id);
			var organizationName = user.Organization;
			var vacancylist= await dbContext.Vacancies.Where(x=>x.PublishedBy==organizationName).ToListAsync();
			return vacancylist;
				}
		/// <summary>
		/// To update the details of any particular vacancy
		/// </summary>
		/// <param name="vacancy"></param>
		/// <returns></returns>
        public async Task<Vacancy?> UpdateAsync(Vacancy vacancy)
		{
			dbContext.Vacancies.Update(vacancy);
		
			await dbContext.SaveChangesAsync();
			return vacancy;
		}

		/// <summary>
		/// To get list of all the vacancies in the databse for the home page
		/// </summary>
		/// <returns></returns>

        public async Task<List<Vacancy?>> GetAllAsync()
        {
			var vacancylist=await dbContext.Vacancies.ToListAsync();
			return vacancylist;
        }

	
		/// <summary>
		/// To check that whether such job application exist in the database or not
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="vacancyId"></param>
		/// <returns></returns>
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
	

