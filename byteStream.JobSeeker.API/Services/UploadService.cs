
using byteStream.JobSeeker.Api.Data;
using byteStream.JobSeeker.API.Models.Dto;
using byteStream.JobSeeker.API.Services.IServices;
using Microsoft.AspNetCore.Hosting;

namespace byteStream.JobSeeker.API.Services
{

    public class UploadService : IUploadService
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        public UploadService( IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
      
        }
        /// <summary>
        /// To Store the Incoming resume in Local Folder and return back its Url
        /// </summary>
        /// <param name="file"></param>
        /// <param name="resume"></param>
        /// <returns></returns>
        public async Task<UploadDto> Upload(IFormFile file, UploadDto resume)
        {
            var localPath = Path.Combine(webHostEnvironment.ContentRootPath, "Resumes", $"{resume.FileName}{resume.FileExtension}");

            using var stream = new FileStream(localPath, FileMode.Create);
            await file.CopyToAsync(stream);

            var httpRequest = httpContextAccessor.HttpContext.Request;
            var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Resumes/{resume.FileName}{resume.FileExtension}";

            resume.Url = urlPath;
            return resume;
        }

        /// To Store the Incoming Profile Image of JobSeeker in Local Folder and return back its Url

        public async Task<UploadDto> UploadImage(IFormFile file, UploadDto image)
        {
            var localPath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");
            FileInfo oldFile = new FileInfo(localPath);
            if (oldFile.Exists)
            {
                oldFile.Delete();
            }

            using var stream = new FileStream(localPath, FileMode.Create);
            await file.CopyToAsync(stream);

            var httpRequest = httpContextAccessor.HttpContext.Request;
            var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/{image.FileName}{image.FileExtension}";

            image.Url = urlPath;
            return image;
        }
    }
}
