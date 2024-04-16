using byteStream.Employer.API.Data;
using byteStream.Employer.API.Models.Dto;
using byteStream.Employer.API.Services.IServices;
using Microsoft.AspNetCore.Hosting;

namespace byteStream.Employer.API.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ImageService( IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
           this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;

        }

        /// <summary>
        /// To add The InComing image file to local folder and returning its path 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="companyLogoDto"></param>
        /// <returns></returns>
        public async Task<CompanyLogoDto> Upload(IFormFile file, CompanyLogoDto companyLogoDto)
        {
            var localPath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{companyLogoDto.FileName}.png");
            FileInfo oldFile = new FileInfo(localPath);
            if (oldFile.Exists)
            {
                oldFile.Delete();
            }

            using var stream = new FileStream(localPath, FileMode.Create);
            await file.CopyToAsync(stream);

            var httpRequest = httpContextAccessor.HttpContext.Request;
            var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/{companyLogoDto.FileName}.png";

            companyLogoDto.Url = urlPath;
            return companyLogoDto;

        }
    }
}
