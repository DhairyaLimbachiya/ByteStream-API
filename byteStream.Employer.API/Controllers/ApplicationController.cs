﻿using AutoMapper;
using Azure;
using byteStream.Employer.Api.Models;
using byteStream.Employer.API.Models.Dto;
using byteStream.Employer.API.Services;
using byteStream.Employer.API.Services.IServices;
using byteStream.JobSeeker.Api.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace byteStream.Employer.API.Controllers
{
    [Route("api/application")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService applicationService;
        private readonly IProfileService profileService;
        private readonly IMapper mapper;
       // protected ResponseDto response;

        public ApplicationController(IApplicationService applicationService, IMapper mapper, IProfileService profileService)
        {
            this.profileService = profileService;
            this.applicationService = applicationService;
            this.mapper = mapper;
           // response = new ();
        }

        [HttpGet]
        [Route("getAllByUser/{id}")]
        [Authorize]
        public async Task<IActionResult> getApplicationsByUserId([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            var result = await applicationService.GetAllByUserIdAsync(id);
            List<UserVacancyResponseDto> response = [];
            foreach (var application in result)
            {
                response.Add(mapper.Map<UserVacancyResponseDto>(application));
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("getAllByVacancy/{id}")]
        [Authorize]
        public async Task<IActionResult> getApplicationsByVacancyId([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            
           List<UserVacancyRequests> result = await applicationService.GetAllByVacancyIdAsync(id);
                List<UserVacancyResponseDto> response = [];
                List<Guid> usersList = [];

                foreach (var application in result)
                {
                    usersList.Add(application.UserId);
                    response.Add(mapper.Map<UserVacancyResponseDto>(application));
                }

                List<UserDto> users = await profileService.GetUsers(usersList);
                foreach (var item in response)
                {
                    item.User = users.FirstOrDefault(u => u.Id == item.UserId);
                }
            
            return Ok(response);
        }

        [HttpPost]
        [Route("createApplication")]
        [Authorize(Roles = "JobSeeker")]
        public async Task<IActionResult> createApplication([FromBody] UserVacancyRequestDto request)
        {
            if (request == null)
            {
              return BadRequest();
            }


            var dto = mapper.Map<UserVacancyRequests>(request);
            var existingVacancy = await applicationService.GetDetailAsync(dto.UserId, dto.VacancyId);
            if (existingVacancy != null)
            {
                return Ok(null);
            }
            var result = await applicationService.CreateAsync(dto);
            var response = mapper.Map<UserVacancyResponseDto>(result);
            return Ok(response);
        }
    }
}