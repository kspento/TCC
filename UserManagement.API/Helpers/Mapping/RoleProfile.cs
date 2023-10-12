using AutoMapper;
using UserManagement.Data.Dto.Role;
using UserManagement.Data.Dto.RoleClaim;
using UserManagement.Data.Entities;

namespace UserManagement.API.Helpers.Mapping
{
    public class RoleProfile: Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleClaim, RoleClaimDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<AddRoleCommand, Role>();
            CreateMap<UpdateRoleCommand, Role>();
        }
    }
}
