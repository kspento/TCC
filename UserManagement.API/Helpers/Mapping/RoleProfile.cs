using AutoMapper;
using UserManagement.Data.Dto.Role;
using UserManagement.Data.Dto.RoleClaim;
using UserManagement.Data.Entities;
using UserManagement.Domain.Model.Role;

namespace UserManagement.API.Helpers.Mapping
{
    public class RoleProfile: Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleClaim, RoleClaimDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<AddRoleModel, Role>();
            CreateMap<UpdateRoleModel, Role>();
        }
    }
}
