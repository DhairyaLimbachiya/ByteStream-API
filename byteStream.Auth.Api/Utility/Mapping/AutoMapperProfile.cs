using AutoMapper;
using byteStream.Auth.Api.Models;
using System.Drawing;

namespace byteStream.Auth.Api.Utility.Mapping
{
	public class AutoMapperProfile :Profile
	{
        public AutoMapperProfile()
        {
			CreateMap< RegisterRequestDto,ApplicationUser >()
				.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
				.ForMember(dest => dest.NormalizedUserName, opt => opt.MapFrom(src => src.Email.ToUpper()))
				.ReverseMap();
			CreateMap<UserDto,ApplicationUser>().ReverseMap();

		}
    }
}
