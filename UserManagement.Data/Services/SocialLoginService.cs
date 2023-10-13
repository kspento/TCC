﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Threading;
using System;
using UserManagement.Data.Dto.LoginAudit;
using UserManagement.Data.Dto.User;
using UserManagement.Data.Entities;
using UserManagement.Helper;
using AutoMapper;
using UserManagement.Data.Hub;
using UserManagement.Data.Repository.Contracts;
using UserManagement.Domain.Model.Social;
using System.Linq;
using UserManagement.Domain.Contracts.Services;

namespace UserManagement.Domain.Services
{
    public class SocialLoginService : ISocialLoginService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ILoginAuditRepository _loginAuditRepository;
        private readonly IHubContext<UserHub, IHubClient> _hubContext;
        private readonly IRoleRepository _roleRepository;

        public SocialLoginService(IMapper mapper,
            UserManager<User> userManager,
            IUserRepository userRepository,
            ILoginAuditRepository loginAuditRepository,
             IHubContext<UserHub, IHubClient> hubContext,
             IRoleRepository roleRepository)
        {
            _mapper = mapper;
            _userManager = userManager;
            _userRepository = userRepository;
            _loginAuditRepository = loginAuditRepository;
            _hubContext = hubContext;
            _roleRepository = roleRepository;
        }

        public async Task<UserAuthDto> SocialLogin(SocialLoginModel request)
        {
            var loginAudit = new LoginAuditDto
            {
                UserName = request.UserName,
                RemoteIP = request.RemoteIp,
                Status = LoginStatus.Success.ToString(),
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                Provider = request.Provider
            };

            var appUser = await _userManager.FindByNameAsync(request.Email);
            if (appUser != null)
            {
                appUser.FirstName = request.FirstName;
                appUser.LastName = request.LastName;
                await _userManager.UpdateAsync(appUser);
            }
            else
            {
                var entity = _mapper.Map<User>(request);
                entity.Id = Guid.NewGuid();
                entity.IsActive = true;

                // Assign Social medial Role to user
                var socialMediaRole = _roleRepository.All.Where(c => c.Name.ToLower() == "social media").FirstOrDefault();
                if (socialMediaRole != null)
                {
                    entity.UserRoles.Add(new UserRole
                    {
                        RoleId = socialMediaRole.Id,
                        UserId = entity.Id
                    });
                }

                IdentityResult result = await _userManager.CreateAsync(entity);
                if (!result.Succeeded)
                {
                    loginAudit.Status = LoginStatus.Error.ToString();
                    await _loginAuditRepository.LoginAudit(loginAudit);
                    throw new System.Exception();
                }
                appUser = await _userManager.FindByNameAsync(request.Email);
            }

            await _loginAuditRepository.LoginAudit(loginAudit);
            var authUser = await _userRepository.BuildUserAuthObject(appUser);
            var onlineUser = new SignlarUser
            {
                Email = authUser.Email,
                Id = authUser.Id.ToString()
            };
            await _hubContext.Clients.All.Joined(onlineUser);
            return authUser;
        }
    }
}
