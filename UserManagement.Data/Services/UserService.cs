using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Data.Context;
using UserManagement.Data.Dto.LoginAudit;
using UserManagement.Data.Dto.User;
using UserManagement.Data.Dto.UserClaim;
using UserManagement.Data.Entities;
using UserManagement.Data.Hub;
using UserManagement.Data.Repository.Contracts;
using UserManagement.Data.UnitOfWork;
using UserManagement.Domain.Contracts.Services;
using UserManagement.Domain.Model.User;
using UserManagement.Helper;
using UserManagement.Domain.Exception;
using NLog.LayoutRenderers.Wrappers;

namespace UserManagement.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly UserInfoToken _userInfoToken;
        private readonly IMapper _mapper;
        private readonly ILogger<AddUserModel> _logger;
        private readonly SignInManager<User> _signInManager;
        public readonly PathHelper _pathHelper;
        private readonly IUserRepository _userRepository;
        private readonly IUserClaimRepository _userClaimRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IUserAllowedIPRepository _userAllowedIPRepository;
        private readonly ILoginAuditRepository _loginAuditRepository;
        private readonly IHubContext<UserHub, IHubClient> _hubContext;
        IUnitOfWork<UserContext> _uow;

        public UserService(
            UserManager<User> userManager,
            UserInfoToken userInfoToken,
            IMapper mapper,           
            ILogger<AddUserModel> logger,
            PathHelper pathHelper,
            IUserRepository userRepository,
            IUserClaimRepository userClaimRepository,
            IUserRoleRepository userRoleRepository,
            IUserAllowedIPRepository userAllowedIPRepository,
            ILoginAuditRepository loginAuditRepository,
            IHubContext<UserHub, IHubClient> hubContext,
            SignInManager<User> signInManager,
            IUnitOfWork<UserContext> uow
                   
            )
        {
            _mapper = mapper;
            _userManager = userManager;
            _userInfoToken = userInfoToken;
            _logger = logger;
            _userRepository= userRepository;
            _uow = uow;
            _userClaimRepository = userClaimRepository;
            _userRoleRepository = userRoleRepository;
            _userAllowedIPRepository = userAllowedIPRepository;
            _pathHelper = pathHelper;
            _signInManager= signInManager;
            _loginAuditRepository= loginAuditRepository;
            _hubContext= hubContext;
        }

        public async Task<UserDto> AddUser(AddUserModel addUser)
        {
            var appUser = await _userManager.FindByNameAsync(addUser.Email);

            if (appUser != null)
            {
                _logger.LogError("Email already exist for another user.");
                throw new AlreadyExistsException("Email already exist for another user.");
            }
            var entity = _mapper.Map<User>(addUser);
            entity.CreatedBy = Guid.Parse(_userInfoToken.Id);
            entity.ModifiedBy = Guid.Parse(_userInfoToken.Id);
            entity.CreatedDate = DateTime.Now.ToLocalTime();
            entity.ModifiedDate = DateTime.Now.ToLocalTime();
            entity.Id = Guid.NewGuid();
            IdentityResult result = await _userManager.CreateAsync(entity);
            if (!result.Succeeded)
            {
                throw new System.Exception();
            }
            if (!string.IsNullOrEmpty(addUser.Password))
            {
                string code = await _userManager.GeneratePasswordResetTokenAsync(entity);
                IdentityResult passwordResult = await _userManager.ResetPasswordAsync(entity, code, addUser.Password);
                if (!passwordResult.Succeeded)
                {
                    throw new System.Exception();
                }
            }
            return _mapper.Map<UserDto>(entity);
        }

        public async Task<UserDto> ChangePassword(ChangePasswordModel request)
        {
            var result = await _signInManager.PasswordSignInAsync(request.UserName, request.OldPassword, false, false);
            if (!result.Succeeded)
            {
                _logger.LogError("Old Password does not match.");
                throw new NotAllowedException("Old Password does not match.");
            }

            var entity = await _userManager.FindByNameAsync(request.UserName);
            string code = await _userManager.GeneratePasswordResetTokenAsync(entity);
            IdentityResult passwordResult = await _userManager.ResetPasswordAsync(entity, code, request.NewPassword);
            if (!passwordResult.Succeeded)
            {
                throw new System.Exception();
            }

            return _mapper.Map<UserDto>(entity);
        }

        public async Task<UserDto> DeleteUser(DeleteUserModel request)
        {
            var appUser = await _userManager.FindByIdAsync(request.Id.ToString());

            if (appUser == null)
            {
                _logger.LogError("User does not exist.");
                throw new AlreadyExistsException("User does not exist.");
            }
            appUser.IsDeleted = true;
            appUser.DeletedDate = DateTime.Now.ToLocalTime();
            appUser.DeletedBy = Guid.Parse(_userInfoToken.Id);
            IdentityResult result = await _userManager.UpdateAsync(appUser);
            if (!result.Succeeded)
            {
                throw new System.Exception();
            }

            return _mapper.Map<UserDto>(appUser);
        }

        public async Task<List<UserDto>> GetAllUsers()
        {
            var entities = await _userRepository.All.ToListAsync();
            return _mapper.Map<List<UserDto>>(entities);
        }

        public async Task<List<UserDto>> GetRecentlyRegisteredUser()
        {
            var entities = await _userRepository.All.OrderByDescending(c => c.CreatedDate).Take(10).ToListAsync();
            return _mapper.Map<List<UserDto>>(entities);
        }

        public async Task<UserDto> GetUsers(GetUsersModel request)
        {
            var entity = await _userRepository.AllIncluding(c => c.UserRoles, cs => cs.UserClaims, ip => ip.UserAllowedIPs).FirstOrDefaultAsync(c => c.Id == request.UserResource.Id);

            if (entity == null)
            {
                _logger.LogError("User not found");
                throw new NotFoundException("User not found");
            }

            return _mapper.Map<UserDto>(entity);     
        }

        public async Task<UserDto> ResetPassword(ResetPasswordModel request)
        {
            var entity = await _userManager.FindByEmailAsync(request.UserName);
            if (entity == null)
            {
                _logger.LogError("User not Found.");
                throw new NotFoundException("User not Found.");
            }
            string code = await _userManager.GeneratePasswordResetTokenAsync(entity);
            IdentityResult passwordResult = await _userManager.ResetPasswordAsync(entity, code, request.Password);
            if (!passwordResult.Succeeded)
            {
                throw new System.Exception();
            }
            return _mapper.Map<UserDto>(entity);
        }

        public async Task<UserClaimDto> UpdateUserClaim(UpdateUserClaimModel request)
        {
            var appUserClaims = await _userClaimRepository.All.Where(c => c.UserId == request.Id).ToListAsync();
            var claimsToAdd = request.UserClaims.Where(c => !appUserClaims.Select(c => c.ClaimType).Contains(c.ClaimType)).ToList();
            _userClaimRepository.AddRange(_mapper.Map<List<UserClaim>>(claimsToAdd));
            var claimsToDelete = appUserClaims.Where(c => !request.UserClaims.Select(cs => cs.ClaimType).Contains(c.ClaimType)).ToList();
            _userClaimRepository.RemoveRange(claimsToDelete);
            if (await _uow.SaveAsync() <= 0)
            {
                throw new System.Exception();
            }

            //todo retornar userclaims atual atualizado ps. essa lógica tá UMA MERDA
            return _mapper.Map<UserClaimDto>(appUserClaims);
        }

        public async Task<UserDto> UpdateUser(UpdateUserModel request)
        {
            var appUser = await _userManager.FindByIdAsync(request.Id.ToString());
            if (appUser == null)
            {
                _logger.LogError("User does not exist.");
                throw new AlreadyExistsException("User does not exist.");
            }
            appUser.FirstName = request.FirstName;
            appUser.LastName = request.LastName;
            appUser.PhoneNumber = request.PhoneNumber;
            appUser.Address = request.Address;
            appUser.IsActive = request.IsActive;
            appUser.ModifiedDate = DateTime.Now.ToLocalTime();
            appUser.ModifiedBy = Guid.Parse(_userInfoToken.Id);
            IdentityResult result = await _userManager.UpdateAsync(appUser);

            // Update User's Role
            var userRoles = _userRoleRepository.All.Where(c => c.UserId == appUser.Id).ToList();
            var rolesToAdd = request.UserRoles.Where(c => !userRoles.Select(c => c.RoleId).Contains(c.RoleId)).ToList();
            _userRoleRepository.AddRange(_mapper.Map<List<UserRole>>(rolesToAdd));
            var rolesToDelete = userRoles.Where(c => !request.UserRoles.Select(cs => cs.RoleId).Contains(c.RoleId)).ToList();
            _userRoleRepository.RemoveRange(rolesToDelete);

            // Update User's Allowed IPs
            var userAllowedIPs = _userAllowedIPRepository.All.Where(c => c.UserId == appUser.Id).ToList();
            var ipsToAdd = request.UserAllowedIPs
                .Where(c => !userAllowedIPs.Select(c => c.IPAddress).Contains(c.IPAddress))
                .Select(cs => new UserAllowedIP
                {
                    IPAddress = cs.IPAddress,
                    UserId = appUser.Id
                })
                .ToList();
            _userAllowedIPRepository.AddRange(ipsToAdd);
            var ipsToDelete = userAllowedIPs
                .Where(c => !request.UserAllowedIPs.Select(cs => cs.IPAddress).Contains(c.IPAddress))
                .ToList();
            _userAllowedIPRepository.RemoveRange(ipsToDelete);

            if (await _uow.SaveAsync() <= 0 && !result.Succeeded)
            {
                throw new System.Exception();
            }
            return _mapper.Map<UserDto>(appUser);
        }

        public async Task<UserDto> UpdateUserProfile(UpdateUserProfileModel request)
        {
            var appUser = await _userManager.FindByIdAsync(_userInfoToken.Id);
            if (appUser == null)
            {
                _logger.LogError("User does not exist.");
                throw new AlreadyExistsException("User does not exist.");
            }
            appUser.FirstName = request.FirstName;
            appUser.LastName = request.LastName;
            appUser.PhoneNumber = request.PhoneNumber;
            appUser.Address = request.Address;
            IdentityResult result = await _userManager.UpdateAsync(appUser);
            if (await _uow.SaveAsync() <= 0 && !result.Succeeded)
            {
                throw new System.Exception();
            }
            if (!string.IsNullOrWhiteSpace(appUser.ProfilePhoto))
                appUser.ProfilePhoto = $"{_pathHelper.UserProfilePath}/{appUser.ProfilePhoto}";

            return _mapper.Map<UserDto>(appUser);
        }

        public async Task<UserDto> UpdateUserProfilePhoto(UpdateUserProfilePhotoModel request)
        {
            var filePath = $"{request.RootPath}/{_pathHelper.UserProfilePath}";
            var appUser = await _userManager.FindByIdAsync(_userInfoToken.Id);
            if (appUser == null)
            {
                _logger.LogError("User does not exist.");
                throw new AlreadyExistsException("User does not exist.");
            }
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            // delete existing file
            if (!string.IsNullOrWhiteSpace(appUser.ProfilePhoto))
            {
                if (File.Exists($"{filePath}/{appUser.ProfilePhoto}"))
                {
                    File.Delete($"{filePath}/{appUser.ProfilePhoto}");
                }
            }

            // save new file
            if (request.FormFile.Any())
            {
                var profileFile = request.FormFile[0];
                var newProfilePhoto = $"{Guid.NewGuid()}{Path.GetExtension(profileFile.Name)}";
                string fullPath = Path.Combine(filePath, newProfilePhoto);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    profileFile.CopyTo(stream);
                }
                appUser.ProfilePhoto = newProfilePhoto;
            }
            else
            {
                appUser.ProfilePhoto = "";
            }

            // update user
            IdentityResult result = await _userManager.UpdateAsync(appUser);
            if (await _uow.SaveAsync() <= 0 && !result.Succeeded)
            {
                throw new System.Exception();
            }

            if (!string.IsNullOrWhiteSpace(appUser.ProfilePhoto))
                appUser.ProfilePhoto = $"{_pathHelper.UserProfilePath}/{appUser.ProfilePhoto}";


            return _mapper.Map<UserDto>(appUser);
        }

        public async Task<UserAuthDto> UserLogin(UserLoginModel request)
        {
            var authUser = new UserAuthDto();
            var loginAudit = new LoginAuditDto
            {
                UserName = request.UserName,
                RemoteIP = request.RemoteIp,
                Status = LoginStatus.Error.ToString(),
                Latitude = request.Latitude,
                Longitude = request.Longitude
            };
            var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, false, false);
            if (result.Succeeded)
            {
                var userInfo = await _userRepository
                    .AllIncluding(c => c.UserAllowedIPs)
                    .Where(c => c.UserName == request.UserName)
                    .FirstOrDefaultAsync();
                if (!userInfo.IsActive)
                {
                    await _loginAuditRepository.LoginAudit(loginAudit);
                    throw new UnauthorizedException("UserName Or Password is InCorrect.");
                }

                if (userInfo.UserAllowedIPs.Any() && !userInfo.UserAllowedIPs.Any(c => c.IPAddress == request.RemoteIp))
                {
                    await _loginAuditRepository.LoginAudit(loginAudit);
                    throw new UnauthorizedException("You don't have access on this IP Address.");
                }
                loginAudit.Status = LoginStatus.Success.ToString();
                await _loginAuditRepository.LoginAudit(loginAudit);
                authUser = await _userRepository.BuildUserAuthObject(userInfo);
                var onlineUser = new SignlarUser
                {
                    Email = authUser.Email,
                    Id = authUser.Id.ToString()
                };
                await _hubContext.Clients.All.Joined(onlineUser);
               
            }
            else
            {
                await _loginAuditRepository.LoginAudit(loginAudit);
                throw new UnauthorizedException("UserName Or Password is InCorrect.");
            }
            return authUser;
        }

        public async Task<UserDto> GetUser(GetUserModel request)
        {
            var entity = await _userRepository.AllIncluding(c => c.UserRoles, cs => cs.UserClaims, ip => ip.UserAllowedIPs).FirstOrDefaultAsync(c => c.Id == request.Id);
            if (entity != null)
                return _mapper.Map<UserDto>(entity);
            else
            {
                _logger.LogError("User not found");

                throw new NotFoundException("User not found");
            }            
        }
    }
}
