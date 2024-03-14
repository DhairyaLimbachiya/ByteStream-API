using byteStream.JobSeeker.Api.Models;

namespace byteStream.JobSeeker.API.Services.IServices

{
    public interface IQualificationService
	{
		Task<Qualification?> GetByIdAsync(Guid id);
		Task<List<Qualification>?> GetAllAsync(Guid id);
		Task<Qualification> CreateAsync(Qualification qualification);
		Task<Qualification?> UpdateAsync( Qualification qualification);
		Task<Qualification?> DeleteAsync(Guid id);
	}
}
