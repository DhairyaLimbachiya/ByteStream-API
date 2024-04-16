using byteStream.JobSeeker.Api.Data;
using byteStream.JobSeeker.Api.Models;
using byteStream.JobSeeker.API.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace byteStream.JobSeeker.API.Services
{
    public class ExperienceService : IExperienceService
	{
		private readonly AppDbContext dbContext;

		public ExperienceService(AppDbContext dbContext)
		{
			this.dbContext = dbContext;
		}
		/// <summary>
		/// To add new experience in teh database
		/// </summary>
		/// <param name="experience"></param>
		/// <returns></returns>
		public async  Task<Experience> CreateAsync(Experience experience)
		{
			await dbContext.Experiences.AddAsync(experience);
			await dbContext.SaveChangesAsync();
			return experience;
		}
		/// <summary>
		/// To delete a particular Experience from the database
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task<Experience?> DeleteAsync(Guid id)
		{
			var existing = await dbContext.Experiences.FirstOrDefaultAsync(x => x.Id == id);
			if (existing == null) return null;

			dbContext.Experiences.Remove(existing);
			await dbContext.SaveChangesAsync();
			return existing;
		}

		/// <summary>
		/// To get detail of a particulae experience based on the id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task<Experience?> GetByIdAsync(Guid id)
		{
			return await dbContext.Experiences.FirstOrDefaultAsync(x => x.Id == id);

		}
		/// <summary>
		/// To get list of experiences of any specific user
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task<List<Experience>?> GetAllAsync(Guid id)
		{
			return await dbContext.Experiences.Where(x => x.UserID == id).ToListAsync();
		}
		
		/// <summary>
		/// to update any existing experience
		/// </summary>
		/// <param name="experience"></param>
		/// <returns></returns>
		public async Task<Experience?> UpdateAsync(Experience experience)
		{
            var existing = await dbContext.Experiences.FirstOrDefaultAsync(x => x.Id == experience.Id);
			if (existing != null)
			{
                dbContext.Entry(existing).CurrentValues.SetValues(experience);

               
				await dbContext.SaveChangesAsync();
				return experience;
			}
			return null;
		}
	}
}
