using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Data.Dto.User;
using UserManagement.Data.Hub;
using UserManagement.Data.Repository.Contracts;
using UserManagement.Domain.Dto.User;
using UserManagement.Helper;
using UserManagement.MediatR.Queries;
using UserManagement.Repository;

namespace UserManagement.MediatR.Handlers
{
    public class GetOnlineUsersQueryHandler : IRequestHandler<GetOnlineUsersQuery, List<UserDto>>
    {
        private readonly IConnectionMappingRepository _connectionMappingRepository;
        private readonly IUserRepository _userRepository;
        private readonly PathHelper _pathHelper;
        private readonly UserInfoToken _userInfoToken;
        public GetOnlineUsersQueryHandler(IUserRepository userRepository,
            IConnectionMappingRepository connectionMappingRepository,
            PathHelper pathHelper,
            UserInfoToken userInfoToken)
        {
            _userRepository = userRepository;
            _connectionMappingRepository = connectionMappingRepository;
            _pathHelper = pathHelper;
            _userInfoToken = userInfoToken;
        }
        public async Task<List<UserDto>> Handle(GetOnlineUsersQuery request, CancellationToken cancellationToken)
        {
            var user = new SignlarUser
            {
                ConnectionId = _userInfoToken.ConnectionId,
                Email = _userInfoToken.Email,
                Id = _userInfoToken.Id,
            };

            var allUserIds = _connectionMappingRepository.GetAllUsersExceptThis(user).Select(c => Guid.Parse(c.Id)).ToList();
            var users = await _userRepository.All.Where(c => allUserIds.Contains(c.Id))
                .Select(cs => new UserDto
                {
                    Id = cs.Id,
                    FirstName = cs.FirstName,
                    LastName = cs.LastName,
                    Email = cs.Email,
                    ProfilePhoto = !string.IsNullOrWhiteSpace(cs.ProfilePhoto) ? $"{_pathHelper.UserProfilePath}{cs.ProfilePhoto}" : string.Empty
                }).ToListAsync();
            return users;
        }
    }
}
