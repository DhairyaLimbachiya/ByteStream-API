using byteStream.Employer.Api.Models;
using byteStream.Employer.API.Models.Dto;

namespace byteStream.Employer.API.Services.IServices
{

    public interface IApplicationService
    {
        Task<UserVacancyRequests?> GetDetailByIdAsync(Guid id);
        Task<UserVacancyRequests?> GetDetailAsync(Guid userId, Guid vacancyId);
        Task<List<UserVacancyRequests>> GetAllByUserIdAsync(Guid userId);
        Task<List<UserVacancyRequests>> GetAllByVacancyIdAsync(Guid vacancyId);
        Task<UserVacancyRequests> CreateAsync(UserVacancyRequests request);
        Task<List<UserVacancyRequests>> GetAllVacnacyByPageAsync(SP_VacancyRequestDto request);
        Task<UserVacancyRequests?> UpdateAsync(UserVacancyRequests request);
    }
}