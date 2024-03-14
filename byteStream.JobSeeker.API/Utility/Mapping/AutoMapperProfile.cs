using AutoMapper;
using byteStream.JobSeeker.Api.Models;
using byteStream.JobSeeker.Api.Models.Dto;


namespace byteStream.JobSeeker.Api.Utility.Mapping
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			//CreateMap< RegisterRequestDto,ApplicationUser >()
			//	.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
			//	.ForMember(dest => dest.NormalizedUserName, opt => opt.MapFrom(src => src.Email.ToUpper()))
			//	.ReverseMap();

			CreateMap<AddJobSekerDto, JobSeekers>().ReverseMap();
			CreateMap<JobSeekerDto, JobSeekers>().ReverseMap();

            CreateMap<AddQualificationDto, Qualification>().ReverseMap();
            CreateMap<QualificationDto, Qualification>().ReverseMap();

            CreateMap<AddExperienceDto, Experience>().ReverseMap();
            CreateMap<ExperienceDto, Experience>().ReverseMap();




        }
    }
}
