using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Helper;
using Microsoft.Extensions.Logging;
using UserManagement.Data.Context;
using UserManagement.Data.Entities;
using UserManagement.Data.Dto.Role;
using UserManagement.Data.Dto.User;
using UserManagement.Data.UnitOfWork;
using UserManagement.Data.Repository.Contracts;
using UserManagement.Domain.Model.Role;

namespace UserManagement.MediatR.Handlers
{
    public class AddRoleCommandHandler : IRequestHandler<AddRoleModel, ServiceResponse<RoleDto>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork<UserContext> _uow;
        private readonly IMapper _mapper;
        private readonly UserInfoToken _userInfoToken;
        private readonly ILogger<AddRoleCommandHandler> _logger;
        public AddRoleCommandHandler(
           IRoleRepository roleRepository,
            IMapper mapper,
            IUnitOfWork<UserContext> uow,
            UserInfoToken userInfoToken,
            ILogger<AddRoleCommandHandler> logger
            )
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _uow = uow;
            _userInfoToken = userInfoToken;
            _logger = logger;
        }
        public async Task<ServiceResponse<RoleDto>> Handle(AddRoleModel request, CancellationToken cancellationToken)
        {
            var entityExist = await _roleRepository.FindBy(c => c.Name == request.Name).FirstOrDefaultAsync();
            if (entityExist != null)
            {
                _logger.LogError("Role Name already exist.");
                return ServiceResponse<RoleDto>.Return409("Role Name already exist.");
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
                return ServiceResponse<RoleDto>.Return500();
            }
            var entityDto = _mapper.Map<RoleDto>(entity);
            return ServiceResponse<RoleDto>.ReturnResultWith200(entityDto); 
        }
    }
}
