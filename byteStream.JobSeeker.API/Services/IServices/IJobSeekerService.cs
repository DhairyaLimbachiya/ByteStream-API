using byteStream.JobSeeker.Api.Models;

namespace byteStream.JobSeeker.API.Services.IServices
{
    public interface IJobSeekerService
    {
        Task<List<JobSeekers>> GetUsersAsync(List<Guid> userList);
        Task<JobSeekers?> GetByIdAsync(Guid id);
        Task<JobSeekers> CreateAsync(JobSeekers jobSeeker);
        Task<JobSeekers?> UpdateAsync(JobSeekers jobSeeker);
        Task<JobSeekers?> DeleteAsync(Guid id);
    }
}
