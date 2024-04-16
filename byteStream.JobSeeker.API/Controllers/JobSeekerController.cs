using AutoMapper;
using Azure;
using byteStream.JobSeeker.Api.Models;
using byteStream.JobSeeker.Api.Models.Dto;
using byteStream.JobSeeker.API.Models.Dto;
using byteStream.JobSeeker.API.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace byteStream.JobSeeker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobSeekerController : Controller
    {
       
        private readonly IJobSeekerService jobSeekerService;
        private readonly IMapper mapper;
        private readonly IUploadService uploadService;
        protected ResponseDto response;
        public JobSeekerController(IJobSeekerService jobSeekerService,IMapper mapper,IUploadService uploadService)
        {
            this.jobSeekerService = jobSeekerService;
            this.mapper = mapper;
           this.uploadService = uploadService;
            response = new();
        }
        [HttpGet]
        [Route("{id:Guid}")]
       
        
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {  var domain = await jobSeekerService.GetByIdAsync(id);
            if (domain == null) { return NoContent(); }
            var dto = mapper.Map<JobSeekerDto>(domain);
            return Ok(dto);
        }
        [HttpGet]
        [Authorize(Roles = "JobSeeker")]

        public async Task<IActionResult> GetById()
        {
            var id = Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var domain = await jobSeekerService.GetByIdAsync(id);
            if (domain == null) { return NoContent(); }
            var dto = mapper.Map<JobSeekerDto>(domain);
            return Ok(dto);
        }


        [HttpPost]
        [Authorize(Roles = "JobSeeker")]


        public async Task<IActionResult> Create([FromBody] AddJobSekerDto addRequestDto)
        {

            var domain = mapper.Map<JobSeekers>(addRequestDto);
            domain.Id = Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            domain.Email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            domain = await jobSeekerService.CreateAsync(domain);
            var dto = mapper.Map<JobSeekerDto>(domain);
            return Ok(dto);
        }

        [HttpPost]
        [Route("uploadResume")]
        [Authorize(Roles = "JobSeeker")]
        public async Task<IActionResult> UploadResume([FromForm] IFormFile file, [FromForm] string fileName)
        {
            ValidateFileUpload(file);
            if (ModelState.IsValid)
            {
                var resume = new UploadDto
                {
                    FileExtension = Path.GetExtension(file.FileName).ToLower(),
                    FileName = fileName
                };

                resume = await uploadService.Upload(file, resume);
                response.Result = resume.Url;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Resume Upload Model is not Valid";
            }
            return Ok(response);


        }

        [HttpPut]
        [Authorize(Roles = "JobSeeker")]


        public async Task<IActionResult> Update([FromBody] JobSeekerDto updateDto)
        {

            if (ModelState.IsValid)
            {
                var domainModal = mapper.Map<JobSeekers>(updateDto);
                domainModal = await jobSeekerService.UpdateAsync(domainModal);
                if (domainModal == null) { return NotFound(); }
                var dto = mapper.Map<JobSeekerDto>(domainModal);
                return Ok(dto);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost]
        [Authorize(Roles = "JobSeeker")]
        [Route("uploadImage")]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file, [FromForm] string fileName)
        {
            ValidateImageUpload(file);

            if (ModelState.IsValid)
            {
                var image = new UploadDto
                {
                    FileExtension = Path.GetExtension(file.FileName).ToLower(),
                    FileName = fileName
                };

                image = await uploadService.UploadImage(file, image);
               response.Result = image.Url;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Image Upload Model is not Valid";
            }
            return Ok(response);
        }


        private void ValidateImageUpload(IFormFile file)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                ModelState.AddModelError("file", "Unsupported File Format");
            }

            if (file.Length > 10 * 1024 * 1024)
            {
                ModelState.AddModelError("file", "File Size cannot be more than 10MB");
            }
        }
        private void ValidateFileUpload(IFormFile file)
        {
            var allowedExtensions = new string[] { ".pdf", ".doc" };

            if (!allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                ModelState.AddModelError("file", "Unsupported File Format");
            }

            if (file.Length > 5 * 1024 * 1024)
            {
                ModelState.AddModelError("file", "File Size cannot be more than 5MB");
            }
        }


        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "JobSeeker")]


        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var domainModal = await jobSeekerService.DeleteAsync(id);
            if (domainModal == null) { return NotFound(); }
            var dto = mapper.Map<JobSeekerDto>(domainModal);
            return Ok(dto);
        }

        [HttpPost]
        [Route("getUsers")]
        [Authorize]
        public async Task<IActionResult> GetUsers([FromBody] List<Guid> userList)
        {
            List<JobSeekers> users = await jobSeekerService.GetUsersAsync(userList);
            var response = new List<JobSeekerDto>();
            foreach (var user in users)
            {
                response.Add(mapper.Map<JobSeekerDto>(user));
            }
            return Ok(response);
        }

    }
}
