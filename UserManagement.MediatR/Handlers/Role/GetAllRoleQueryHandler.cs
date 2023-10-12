using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Data.Dto.Role;
using UserManagement.Data.Repository.Contracts;
using UserManagement.Domain.Model.Role;

namespace UserManagement.MediatR.Handlers
{
    public class GetAllRoleQueryHandler : IRequestHandler<GetAllRoleModel, List<RoleDto>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllRoleQueryHandler> _logger;

        public GetAllRoleQueryHandler(
           IRoleRepository roleRepository,
            IMapper mapper,
            ILogger<GetAllRoleQueryHandler> logger)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _logger = logger;

        }

        public async Task<List<RoleDto>> Handle(GetAllRoleModel request, CancellationToken cancellationToken)
        {
            var entities = await _roleRepository.All.ToListAsync();
            return _mapper.Map<List<RoleDto>>(entities);
        }
    }
}
