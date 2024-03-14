using byteStream.JobSeeker.API.Models.Dto;

namespace byteStream.JobSeeker.API.Services.IServices
{
    public interface IResumeService
    {
            Task<ResumeDto> Upload(IFormFile file, ResumeDto resume);
        }
    }
