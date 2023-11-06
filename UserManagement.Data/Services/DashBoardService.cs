﻿using System.Threading.Tasks;
using System.Threading;
using UserManagement.Data.Repository.Contracts;
using System.Linq;
using System.Collections.Generic;
using System;
using UserManagement.Data.Dto.User;
using UserManagement.Helper;
using UserManagement.Data.Hub;
using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Contracts.Services;

namespace UserManagement.Domain.Services
{
    public class DashBoardService : IDashBoardService
    {
        private readonly IConnectionMappingRepository _connectionMappingRepository;
        private readonly IUserRepository _userRepository;
        private readonly PathHelper _pathHelper;
        private readonly UserInfoToken _userInfoToken;
        public DashBoardService(IUserRepository userRepository,
            IConnectionMappingRepository connectionMappingRepository,
            PathHelper pathHelper,
            UserInfoToken userInfoToken)
        {
            _userRepository = userRepository;
            _connectionMappingRepository = connectionMappingRepository;
            _pathHelper = pathHelper;
            _userInfoToken = userInfoToken;
        }

        public Task<int> GetActiveUserCount(CancellationToken cancellationToken)
        {
            return Task.FromResult(_userRepository.All.Where(c => c.IsActive).Count());
        }

        public Task<int> GetInactiveUserCount(CancellationToken cancellationToken)
        {
            return Task.FromResult(_userRepository.All.Where(c => !c.IsActive).Count());
        }

        public async Task<List<UserDto>> GetOnlineUsers()
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


        public async Task<int> GetTotalUserCount(CancellationToken cancellationToken)
        {
            return await Task.FromResult(_userRepository.All.Count());
        }
    }
}
