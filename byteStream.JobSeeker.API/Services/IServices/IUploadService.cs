using byteStream.JobSeeker.API.Models.Dto;

namespace byteStream.JobSeeker.API.Services.IServices
{
    public interface IUploadService
    {
            Task<UploadDto> Upload(IFormFile file, UploadDto resume);
             Task<UploadDto> UploadImage(IFormFile file, UploadDto image);
    }
    }
