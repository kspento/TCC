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
    public class GetAllPageQueryHandler : IRequestHandler<GetAllPageQuery, List<PageDto>>
    {
        private readonly IPageRepository _pageRepository;
        private readonly IMapper _mapper;

        public GetAllPageQueryHandler(
            IPageRepository pageRepository,
            IMapper mapper)
        {
            _pageRepository = pageRepository;
            _mapper = mapper;

        }
        public async Task<List<PageDto>> Handle(GetAllPageQuery request, CancellationToken cancellationToken)
        {
            var entities = await _pageRepository.All.ToListAsync();
            return _mapper.Map<List<PageDto>>(entities);
        }
    }
}
