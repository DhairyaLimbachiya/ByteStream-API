using byteStream.JobSeeker.Api.Models;

namespace byteStream.JobSeeker.API.Services.IServices
{
    public interface IExperienceService
	{
		Task<Experience?> GetByIdAsync(Guid id);
		Task<List<Experience>?>  GetAllAsync(Guid id);
		Task<Experience> CreateAsync(Experience experience);
		Task<Experience?> UpdateAsync( Experience experience);
		Task<Experience?> DeleteAsync(Guid id);
	}
}
