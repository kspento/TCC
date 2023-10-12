using AutoMapper;
using UserManagement.Data.Dto.User;
using UserManagement.Data.Dto.UserClaim;
using UserManagement.Data.Entities;
using UserManagement.Domain.Model.Social;
using UserManagement.Domain.Model.User;

namespace UserManagement.API.Helpers.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserClaimDto, UserClaim>().ReverseMap();
            CreateMap<UserRoleDto, UserRole>().ReverseMap();
            CreateMap<UserAllowedIPDto, UserAllowedIP>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<AddUserModel, User>();
            CreateMap<SocialLoginCommand, User>();
            CreateMap<ResetPasswordCommand, UserDto>();
        }
    }
}
