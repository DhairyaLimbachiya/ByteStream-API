using AutoMapper;
using byteStream.Employer.Api.Utility.ApiFilter;
using byteStream.Employer.API.Models;
using byteStream.Employer.API.Models.Dto;
using byteStream.Employer.API.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ByteStream.Employer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacancyController : ControllerBase
    {
        private readonly IVacancyService vacancyService;
        private readonly IMapper mapper;
        private readonly IEmployerService employerService;
        public VacancyController(IVacancyService vacancyService, IMapper mapper, IEmployerService employerService)
        {
            this.vacancyService = vacancyService;
            this.mapper = mapper;
            this.employerService = employerService;

        }

        [HttpGet]
        [Route("GetAll")]
        [Authorize]
        public async Task<IActionResult> GetAllVacancies()
        {
            var vacancyList = await vacancyService.GetAllAsync();
            return Ok(vacancyList);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            bool IsApplied;
            Guid UserID = Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value); 
            IsApplied = await vacancyService.CheckApplicationAsync(UserID, id);
            var domain = await vacancyService.GetByIdAsync(id);
            if (domain == null) { return NotFound(); }
            var dto = mapper.Map<VacancyDto>(domain);
            dto.AlreadyApplied = IsApplied;
            return Ok(dto);
        }



        [HttpGet]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> GetByCompany()
        {
            var Id = Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var vacancyList = await vacancyService.GetByCompanyAsync(Id);
            if (vacancyList.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(vacancyList);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Employer")]


        public async Task<IActionResult> Create([FromBody] AddVacancyDto addRequestDto)
        {

            var domain = mapper.Map<Vacancy>(addRequestDto);
            domain.PublishedDate = DateTime.UtcNow;
            var userId = Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            domain.PublishedBy = await employerService.GetOrganizationName(userId);
            domain = await vacancyService.CreateAsync(domain);
            var dto = mapper.Map<VacancyDto>(domain);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);


        }

        [HttpPut]
        [Authorize(Roles = "Employer")]

        public async Task<IActionResult> Update([FromBody] VacancyDto updateDto)
        {
            
                var domainModal = mapper.Map<Vacancy>(updateDto);

                domainModal = await vacancyService.UpdateAsync(domainModal);
                if (domainModal == null) { return NotFound(); }
                var dto = mapper.Map<VacancyDto>(domainModal);
                return Ok(dto);
            
        }




        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Employer")]

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var domainModal = await vacancyService.DeleteAsync(id);
            if (domainModal == null) { return NotFound(); }
            var dto = mapper.Map<VacancyDto>(domainModal);
            return Ok(dto);
        }
    }


}
