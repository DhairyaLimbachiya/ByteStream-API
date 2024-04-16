using AutoMapper;
using byteStream.JobSeeker.Api.Models;
using byteStream.JobSeeker.Api.Models.Dto;
using byteStream.JobSeeker.API.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace byteStream.JobSeeker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperienceController : ControllerBase
    {
        private readonly IExperienceService experienceService;
        private readonly IMapper mapper;

        public ExperienceController(IExperienceService experienceService, IMapper mapper)
        {
            this.experienceService = experienceService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "JobSeeker")]
        [Route("{id:Guid}")]

        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var domain = await experienceService.GetByIdAsync(id);
            if (domain == null) { return NotFound(); }
            var dto = mapper.Map<ExperienceDto>(domain);
            return Ok(dto);
        }

       
        [HttpGet]
        [Authorize(Roles = "JobSeeker")]
        public async Task<IActionResult> GetAll()
        {
            var id = Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var domain = await experienceService.GetAllAsync(id);
            var dto = mapper.Map<List<ExperienceDto>>(domain);
            if (dto.Count == 0) { return NoContent(); }
            else
            {
                return Ok(dto);
            }
           
        }

       
        [HttpPost]
        [Authorize(Roles = "JobSeeker")]
        public async Task<IActionResult> Create([FromBody] AddExperienceDto addRequestDto)
        {
            var domain = mapper.Map<Experience>(addRequestDto);
            domain.UserID = Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            domain = await experienceService.CreateAsync(domain);
            var dto = mapper.Map<ExperienceDto>(domain);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }
        [HttpPut]
        [Authorize(Roles = "JobSeeker")]
        public async Task<IActionResult> Update([FromBody] ExperienceDto updateDto)
        {
            if (ModelState.IsValid)
            {
                var domainModal = mapper.Map<Experience>(updateDto);
                domainModal.UserID = Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                domainModal = await experienceService.UpdateAsync(domainModal);
                if (domainModal == null) { return NotFound(); }
                var dto = mapper.Map<ExperienceDto>(domainModal);
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
            var domainModal = await experienceService.DeleteAsync(id);
            if (domainModal == null) { return NotFound(); }
            var dto = mapper.Map<ExperienceDto>(domainModal);
            return Ok(dto);
        }

    }
}
