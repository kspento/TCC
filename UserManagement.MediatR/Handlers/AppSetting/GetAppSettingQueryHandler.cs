using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Data.Dto.AppSetting;
using UserManagement.Helper;
using UserManagement.MediatR.Queries;
using UserManagement.Repository;

namespace UserManagement.MediatR.Handlers
{
    public class GetAppSettingQueryHandler : IRequestHandler<GetAppSettingQuery, ServiceResponse<AppSettingDto>>
    {
        private readonly IAppSettingRepository _appSettingRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAppSettingByKeyQueryHandler> _logger;
        public GetAppSettingQueryHandler(
           IAppSettingRepository appSettingRepository,
            IMapper mapper,
             ILogger<GetAppSettingByKeyQueryHandler> logger
            )
        {
            _appSettingRepository = appSettingRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ServiceResponse<AppSettingDto>> Handle(GetAppSettingQuery request, CancellationToken cancellationToken)
        {
            var appsetting = await _appSettingRepository.FindBy(c => c.Id == request.Id).FirstOrDefaultAsync();
            if (appsetting == null)
            {
                _logger.LogError("AppSetting key is not available");
                return ServiceResponse<AppSettingDto>.Return404();
            }
            return ServiceResponse<AppSettingDto>.ReturnResultWith200(_mapper.Map<AppSettingDto>(appsetting));
        }
    }
}
