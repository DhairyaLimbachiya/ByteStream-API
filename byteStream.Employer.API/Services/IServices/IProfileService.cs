using byteStream.JobSeeker.Api.Models.Dto;

namespace byteStream.Employer.API.Services.IServices
{
    public interface IProfileService
    {
        Task<List<UserDto>> GetUsers(List<Guid> users);
    }
}
