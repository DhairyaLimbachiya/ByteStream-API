using byteStream.Employer.API.Models.Dto;

namespace byteStream.Employer.API.Services.IServices
{
    public interface IImageService
    {
        Task<CompanyLogoDto> Upload(IFormFile file, CompanyLogoDto companyLogoDtp);
    }
}
