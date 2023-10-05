using AutoMapper;
using UserManagement.MediatR.Queries;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Helper;
using UserManagement.Data.Dto.PageAction;
using UserManagement.Data.Repository.PageAction;

namespace UserManagement.MediatR.Handlers
{
    public class GetPageActionQueryHandler : IRequestHandler<GetPageActionQuery, ServiceResponse<PageActionDto>>
    {
        private readonly IPageActionRepository _pageActionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPageActionQueryHandler> _logger;

        public GetPageActionQueryHandler(
         IPageActionRepository pageActionRepository,
          IMapper mapper,
          ILogger<GetPageActionQueryHandler> logger)
        {
            _pageActionRepository = pageActionRepository;
            _mapper = mapper;
            _logger = logger;

        }

        public async Task<ServiceResponse<PageActionDto>> Handle(GetPageActionQuery request, CancellationToken cancellationToken)
        {
            var entity = await _pageActionRepository.FindAsync(request.Id);
            if (entity != null)
                return ServiceResponse<PageActionDto>.ReturnResultWith200(_mapper.Map<PageActionDto>(entity));
            else
            {
                _logger.LogWarning("Role Not Found");
                return ServiceResponse<PageActionDto>.Return404();
            }
        }
    }
}
