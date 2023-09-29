using AutoMapper;
using System.Security.Claims;
using UserManagement.Data.Dto.User;

namespace UserManagement.API.Helpers.Mapping
{
    public class AppClaimProfile : Profile
    {
        public AppClaimProfile()
        {
            CreateMap<AppClaimDto, Claim>()
               .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.ClaimType))
               .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.ClaimValue))
               .ReverseMap();

          
        }
    }
}