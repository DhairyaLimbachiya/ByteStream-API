﻿using AutoMapper;
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

		public EmployerController(IEmployerService employerService,IMapper mapper)
        {
			this.employerService = employerService;
			this.mapper = mapper;
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
			domain.Id = Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
			domain.CreatedBy=HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
			domain = await employerService.CreateAsync(domain);
			var dto = mapper.Map<EmployerDto>(domain);
			return CreatedAtAction(nameof(GetById), new { id = dto.ID }, dto);


		}





		[HttpPut]
	
		[Authorize(Roles = "Employer")]
		public async Task<IActionResult> Update([FromBody] EmployerDto updateDto)
		{
			if (ModelState.IsValid)
			{
				var domainModal = mapper.Map<Employeer>(updateDto);
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