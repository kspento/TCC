using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Data.Dto.User;
using UserManagement.Data.Repository.Contracts;
using UserManagement.Domain.Model.Role;

namespace UserManagement.MediatR.Handlers
{
    public class GetRoleUsersHandler : IRequestHandler<GetRoleUsersModel, List<UserRoleDto>>
    {
        IUserRoleRepository _userRoleRepository;
        public GetRoleUsersHandler(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }
        public Task<List<UserRoleDto>> Handle(GetRoleUsersModel request, CancellationToken cancellationToken)
        {
            var userRoles = _userRoleRepository
                .AllIncluding(c => c.User)
                .Where(c => c.RoleId == request.RoleId)
                .Select(cs => new UserRoleDto
                {
                    UserId = cs.UserId,
                    RoleId = cs.RoleId,
                    UserName = cs.User.UserName,
                    FirstName = cs.User.FirstName,
                    LastName = cs.User.LastName
                }).ToList();

            return Task.FromResult(userRoles);

        }
    }
}
