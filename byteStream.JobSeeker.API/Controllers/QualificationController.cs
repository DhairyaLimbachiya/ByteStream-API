using AutoMapper;
using byteStream.JobSeeker.Api.Models;
using byteStream.JobSeeker.Api.Models.Dto;
using byteStream.JobSeeker.API.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ByteStream.JobSeeker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QualificationController : ControllerBase
    {
        private readonly IQualificationService qualificationService;
        private readonly IMapper mapper;

        public QualificationController(IQualificationService qualificationService, IMapper mapper)
        {
            this.qualificationService = qualificationService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "JobSeeker")]

        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {

            var domain = await qualificationService.GetByIdAsync(id);
            if (domain == null) { return NotFound(); }
            var dto = mapper.Map<QualificationDto>(domain);
            return Ok(dto);
        }




        [HttpPost]
        [Authorize(Roles = "JobSeeker")]

        public async Task<IActionResult> Create([FromBody] AddQualificationDto addRequestDto)
        {
            var domain = mapper.Map<Qualification>(addRequestDto);

            domain.UserID = Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            domain = await qualificationService.CreateAsync(domain);
            var dto = mapper.Map<QualificationDto>(domain);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }


        [HttpGet]

        [Authorize(Roles = "JobSeeker")]

        public async Task<IActionResult> GetAll()
        {
            var id = Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var domain = await qualificationService.GetAllAsync(id);
            var dto = mapper.Map<List<QualificationDto>>(domain);
            if (dto.Count==0) { return NoContent(); }
            else
            {
                return Ok(dto);
            }
        }





        [HttpPut]
        [Authorize(Roles = "JobSeeker")]
        public async Task<IActionResult> Update([FromBody] QualificationDto updateDto)
        {
            if (ModelState.IsValid)
            {
                var domainModal = mapper.Map<Qualification>(updateDto);
                domainModal.UserID = Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                domainModal = await qualificationService.UpdateAsync(domainModal);
                if (domainModal == null) { return NotFound(); }
                var dto = mapper.Map<QualificationDto>(domainModal);
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
            var domainModal = await qualificationService.DeleteAsync(id);
            if (domainModal == null) { return NotFound(); }
            var dto = mapper.Map<QualificationDto>(domainModal);
            return Ok(dto);
        }
    }
}
