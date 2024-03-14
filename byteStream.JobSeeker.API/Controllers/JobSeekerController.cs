using AutoMapper;
using Azure;
using byteStream.JobSeeker.Api.Models;
using byteStream.JobSeeker.Api.Models.Dto;
using byteStream.JobSeeker.Api.Utility.ApiFilter;
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
        private readonly IResumeService resumeService;
        protected ResponseDto response;
        public JobSeekerController(IJobSeekerService jobSeekerService,IMapper mapper,IResumeService resumeService)
        {
            this.jobSeekerService = jobSeekerService;
            this.mapper = mapper;
           this.resumeService = resumeService;
            response = new();
        }
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize]
        
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            //var id = Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var domain = await jobSeekerService.GetByIdAsync(id);
            if (domain == null) { return NotFound(); }
            var dto = mapper.Map<JobSeekerDto>(domain);
            return Ok(dto);
        }
        [HttpGet]
        //[Route("{id:Guid}")]
        [Authorize]
        public async Task<IActionResult> GetById()
        {
            var id = Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var domain = await jobSeekerService.GetByIdAsync(id);
            if (domain == null) { return NotFound(); }
            var dto = mapper.Map<JobSeekerDto>(domain);
            return Ok(dto);
        }


        [HttpPost]
        [ValidateModel]
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
            if (ModelState.IsValid)
            {
                var resume = new ResumeDto
                {
                    FileExtension = Path.GetExtension(file.FileName).ToLower(),
                    FileName = fileName
                };

                resume = await resumeService.Upload(file, resume);
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
        //[Route("{id:Guid}")]
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
