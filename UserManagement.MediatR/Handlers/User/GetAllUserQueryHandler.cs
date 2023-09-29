using AutoMapper;
using UserManagement.Data.Dto;
using UserManagement.MediatR.Queries;
using UserManagement.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace UserManagement.MediatR.Handlers
{
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, List<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllUserQueryHandler(
           IUserRepository userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            var entities = await _userRepository.All.ToListAsync();
            return _mapper.Map<List<UserDto>>(entities);
        }
    }
}
