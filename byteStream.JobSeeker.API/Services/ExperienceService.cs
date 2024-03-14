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
		public async  Task<Experience> CreateAsync(Experience experience)
		{
			await dbContext.Experiences.AddAsync(experience);
			await dbContext.SaveChangesAsync();
			return experience;
		}

		public async Task<Experience?> DeleteAsync(Guid id)
		{
			var existing = await dbContext.Experiences.FirstOrDefaultAsync(x => x.Id == id);
			if (existing == null) return null;

			dbContext.Experiences.Remove(existing);
			await dbContext.SaveChangesAsync();
			return existing;
		}

		public async Task<Experience?> GetByIdAsync(Guid id)
		{
			return await dbContext.Experiences.FirstOrDefaultAsync(x => x.Id == id);

		}
		public async Task<List<Experience>?> GetAllAsync(Guid id)
		{
			return await dbContext.Experiences.Where(x => x.UserID == id).ToListAsync();
		}

		public async Task<Experience?> UpdateAsync(Experience experience)
		{
			dbContext.Experiences.Update(experience);
			await dbContext.SaveChangesAsync();
			return experience;
		}
	}
}
