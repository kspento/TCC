using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Data.Context;
using UserManagement.Data.Dto.Role;
using UserManagement.Data.Dto.User;
using UserManagement.Data.Entities;
using UserManagement.Data.Repository.Contracts;
using UserManagement.Data.Repository.RoleClaim;
using UserManagement.Data.UnitOfWork;
using UserManagement.Domain.Exception;
using UserManagement.Domain.Model.Role;
using UserManagement.Helper;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;
    private readonly IRoleClaimRepository _roleClaimRepository;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IUnitOfWork<UserContext> _uow;
    private readonly IMapper _mapper;
    private readonly UserInfoToken _userInfoToken;
    private readonly ILogger<RoleService> _logger;

    public RoleService(
        IRoleRepository roleRepository,
        IRoleClaimRepository roleClaimRepository,
        IUserRoleRepository userRoleRepository,
        IUnitOfWork<UserContext> uow,
        IMapper mapper,
        UserInfoToken userInfoToken,
        ILogger<RoleService> logger)
    {
        _roleRepository = roleRepository;
        _roleClaimRepository = roleClaimRepository;
        _userRoleRepository = userRoleRepository;
        _uow = uow;
        _mapper = mapper;
        _userInfoToken = userInfoToken;
        _logger = logger;
    }

    // Method to handle UpdateRoleCommand
    public async Task<RoleDto> UpdateRole(UpdateRoleModel request)
    {
        var entityExist = await _roleRepository.FindBy(c => c.Id != request.Id)
          .FirstOrDefaultAsync();

        //if (entityExist != null)
        //{
        //    _logger.LogError("Role Name Already Exist.");
        //    throw new AlreadyExistsException("Role Name Already Exist.");
        //}

        entityExist = await _roleRepository.FindByInclude(v => v.Id == request.Id, c => c.RoleClaims).FirstOrDefaultAsync();
        entityExist.Name = request.Name;
        entityExist.ModifiedBy = Guid.Parse(_userInfoToken.Id);
        entityExist.ModifiedDate = DateTime.Now.ToLocalTime();
        entityExist.NormalizedName = request.Name;
        _roleRepository.Update(entityExist);

        var roleClaims = entityExist.RoleClaims.ToList();
        var roleClaimsToAdd = request.RoleClaims.Where(c => !roleClaims.Select(c => c.Id).Contains(c.Id)).ToList();
        _roleClaimRepository.AddRange(_mapper.Map<List<RoleClaim>>(roleClaimsToAdd));
        var roleClaimsToDelete = roleClaims.Where(c => !request.RoleClaims.Select(cs => cs.Id).Contains(c.Id)).ToList();
        _roleClaimRepository.RemoveRange(roleClaimsToDelete);

        if (await _uow.SaveAsync() <= 0)
        {
            throw new System.Exception();
        }

        return _mapper.Map<RoleDto>(entityExist);
    }

    // Method to handle GetRoleUsersQuery
    public async Task<List<UserRoleDto>> GetRoleUsers(Guid roleId)
    {
        var userRoles = _userRoleRepository
            .AllIncluding(c => c.User)
            .Where(c => c.RoleId == roleId)
            .Select(cs => new UserRoleDto
            {
                UserId = cs.UserId,
                RoleId = cs.RoleId,
                UserName = cs.User.UserName,
                FirstName = cs.User.FirstName,
                LastName = cs.User.LastName
            }).ToList();

        return userRoles;
    }

    // Method to handle GetRoleQuery
    public async Task<RoleDto> GetRole(Guid id)
    {
        var entity = await _roleRepository.AllIncluding(c => c.UserRoles, cs => cs.RoleClaims)
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();

        if (entity != null)
        {
            var result = _mapper.Map<RoleDto>(entity);
            return result;
        }
        else
        {
            _logger.LogError("Not found");
            throw new NotFoundException(string.Empty);
        }
    }

    // Method to handle GetAllRoleQuery
    public async Task<List<RoleDto>> GetAllRoles()
    {
        var entities = await _roleRepository.All.ToListAsync();

        return _mapper.Map<List<RoleDto>>(entities);
    }

    // Method to handle DeleteRoleCommand
    public async Task DeleteRole(Guid id)
    {
        var entityExist = await _roleRepository.FindAsync(id);
        if (entityExist == null)
        {
            _logger.LogError("Not found");
            throw new NotFoundException(string.Empty);
        }

        entityExist.IsDeleted = true;
        entityExist.DeletedBy = Guid.Parse(_userInfoToken.Id);
        entityExist.DeletedDate = DateTime.Now.ToLocalTime();
        _roleRepository.Update(entityExist);

        if (await _uow.SaveAsync() <= 0)
        {
            throw new System.Exception();
        }
    }

    // Method to handle AddRoleCommand
    public async Task<RoleDto> AddRole(AddRoleModel request)
    {
        var entityExist = await _roleRepository.FindBy(c => c.Name == request.Name).FirstOrDefaultAsync();
        if (entityExist != null)
        {
            _logger.LogError("Role Name already exist.");
            throw new AlreadyExistsException("Role Name already exist.");
        }

        var entity = _mapper.Map<Role>(request);
        entity.Id = Guid.NewGuid();
        entity.CreatedBy = Guid.Parse(_userInfoToken.Id);
        entity.CreatedDate = DateTime.Now.ToLocalTime();
        entity.ModifiedBy = Guid.Parse(_userInfoToken.Id);
        entity.NormalizedName = entity.Name;
        _roleRepository.Add(entity);

        if (await _uow.SaveAsync() <= 0)
        {
            throw new System.Exception();
        }

        var entityDto = _mapper.Map<RoleDto>(entity);

        return entityDto;
    }

    public async Task UpdateRoleUser(UpdateRoleModel request)
    {
        var userRoles = await _userRoleRepository.All.Where(c => c.RoleId == request.Id).ToListAsync();
        var userRolesToAdd = request.UserRoles.Where(c => !userRoles.Select(c => c.UserId).Contains(c.UserId.Value)).ToList();
        
        _userRoleRepository.AddRange(_mapper.Map<List<UserRole>>(userRolesToAdd));
        
        var userRolesToDelete = userRoles.Where(c => !request.UserRoles.Select(cs => cs.UserId).Contains(c.UserId)).ToList();
        
        _userRoleRepository.RemoveRange(userRolesToDelete);

        if (await _uow.SaveAsync() <= 0)
        {
            //return ServiceResponse<UserRoleDto>.Return500();
            throw new Exception();
        }
    }
}
