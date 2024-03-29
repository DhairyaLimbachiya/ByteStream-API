using AutoMapper;
using Azure;
using byteStream.Employer.Api.Models;
using byteStream.Employer.Api.Utility.ApiFilter;
using byteStream.Employer.API.Models.Dto;
using byteStream.Employer.API.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Drawing;
using System.Security.Claims;

namespace ByteStream.Employer.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmployerController : ControllerBase
	{
		private readonly IEmployerService employerService;
		private readonly IMapper mapper;
		private readonly IImageService imageService;
        protected ResponseDto response;
        public EmployerController(IEmployerService employerService,IMapper mapper,IImageService imageService)
        {
			this.employerService = employerService;
			this.mapper = mapper;
			this.imageService=imageService;
            response = new();
        }

		[HttpGet]
		[Route("GetByCompanyName/{companyName}")]

		public async Task<IActionResult> GetByCompany([FromRoute]string companyName)
		{
			if(companyName == null) {
				return BadRequest();
			}
		var domain=	await employerService.GetByCompanyName(companyName);
            var dto = mapper.Map<EmployerDto>(domain);
			return Ok(dto);
        }

        [HttpGet]
		[Authorize]

		public async Task<IActionResult> GetById()
		{
			var Id = Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
			var domain = await employerService.GetByIdAsync(Id);
			if (domain == null) { return NotFound(); }
			var dto = mapper.Map<EmployerDto>(domain);
			return Ok(dto);
		}


		[HttpPost]
		
		[Authorize(Roles = "Employer")]
		public async Task<IActionResult> Create([FromBody] AddEmployerDto addRequestDto)
		{

			var domain = mapper.Map<Employeer>(addRequestDto);
            var nameIdentifierClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            domain.Id = Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
			domain.CreatedBy=HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
			domain = await employerService.CreateAsync(domain);
			var dto = mapper.Map<EmployerDto>(domain);
			return CreatedAtAction(nameof(GetById), new { id = dto.ID }, dto);
		}

        [HttpPost]
        [Route("uploadImage")]
        [Authorize(Roles = "Employer")]

        public async Task<IActionResult> UploadImage([FromForm] IFormFile file, [FromForm] string fileName)
        {
            ValidateFileUpload(file);

            if (ModelState.IsValid)
            {
                var resume = new CompanyLogoDto
                {
                    FileExtension = Path.GetExtension(file.FileName).ToLower(),
                    FileName = fileName
                };

                resume = await imageService.Upload(file, resume);
                response.Result = resume.Url;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Image Upload Model is not Valid";
            }
            return Ok(response);
        }
        private void ValidateFileUpload(IFormFile file)
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




        [HttpPut]
	
		[Authorize(Roles = "Employer")]
		public async Task<IActionResult> Update([FromBody] EmployerDto updateDto)
		{
			if (ModelState.IsValid)
			{
				var domainModal = mapper.Map<Employeer>(updateDto);
                domainModal.CreatedBy = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
                domainModal.Id = Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                domainModal = await employerService.UpdateAsync( domainModal);
				if (domainModal == null) { return NotFound(); }
				var dto = mapper.Map<EmployerDto>(domainModal);
				return Ok(dto);
			}
			else
			{
				return BadRequest(ModelState);
			}
		}




		[HttpDelete]
		[Route("{id:Guid}")]
		[Authorize(Roles = "Employer")]

		public async Task<IActionResult> Delete([FromRoute] Guid id)
		{
			var domainModal = await employerService.DeleteAsync(id);
			if (domainModal == null) { return NotFound(); }
			var dto = mapper.Map<EmployerDto>(domainModal);
			return Ok(dto);
		}
	}
}
