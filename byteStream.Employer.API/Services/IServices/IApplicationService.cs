using byteStream.Employer.Api.Models;

namespace byteStream.Employer.API.Services.IServices
{

    public interface IApplicationService
    {
        Task<UserVacancyRequests?> GetDetailAsync(Guid userId, Guid vacancyId);
        Task<List<UserVacancyRequests>> GetAllByUserIdAsync(Guid userId);
        Task<List<UserVacancyRequests>> GetAllByVacancyIdAsync(Guid vacancyId);
        Task<UserVacancyRequests> CreateAsync(UserVacancyRequests request);
    }
}