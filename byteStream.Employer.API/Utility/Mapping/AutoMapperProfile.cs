using AutoMapper;
using byteStream.Employer.Api.Models;
using byteStream.Employer.API.Models;
using byteStream.Employer.API.Models.Dto;

namespace byteStream.Employer.Api.Utility.Mapping
{
	public class AutoMapperProfile :Profile
	{
        public AutoMapperProfile()
        {
            CreateMap<VacancyDto, Vacancy>().ReverseMap();
            CreateMap<AddVacancyDto, Vacancy>().ReverseMap();
           
            CreateMap<Employeer, EmployerDto>().ReverseMap();
            CreateMap<Employeer, AddEmployerDto>().ReverseMap();

            CreateMap<UserVacancyRequests,UserVacancyRequestDto>().ReverseMap();
            CreateMap<UserVacancyRequestDto,UserVacancyRequests>().ReverseMap();
            CreateMap<Vacancy,UserVacancyResponseDto>().ReverseMap();
            CreateMap<UserVacancyRequests, UserVacancyResponseDto>().ReverseMap();


        }
    }
}
