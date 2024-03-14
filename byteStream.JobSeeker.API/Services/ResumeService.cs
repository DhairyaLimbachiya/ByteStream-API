
using byteStream.JobSeeker.Api.Data;
using byteStream.JobSeeker.API.Models.Dto;
using byteStream.JobSeeker.API.Services.IServices;

namespace byteStream.JobSeeker.API.Services
{

    public class ResumeService : IResumeService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _db;
        public ResumeService(AppDbContext db, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _db = db;
        }

        public async Task<ResumeDto> Upload(IFormFile file, ResumeDto resume)
        {
            var localPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Resumes", $"{resume.FileName}{resume.FileExtension}");

            using var stream = new FileStream(localPath, FileMode.Create);
            await file.CopyToAsync(stream);

            var httpRequest = _httpContextAccessor.HttpContext.Request;
            var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Resumes/{resume.FileName}{resume.FileExtension}";

            resume.Url = urlPath;
            return resume;
        }
    }
}
