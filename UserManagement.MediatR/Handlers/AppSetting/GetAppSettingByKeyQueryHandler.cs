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
    public class GetAppSettingByKeyQueryHandler : IRequestHandler<GetAppSettingByKeyQuery, ServiceResponse<AppSettingDto>>
    {
        private readonly IAppSettingRepository _appSettingRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAppSettingByKeyQueryHandler> _logger;
        public GetAppSettingByKeyQueryHandler(
           IAppSettingRepository appSettingRepository,
            IMapper mapper,
            ILogger<GetAppSettingByKeyQueryHandler> logger
            )
        {
            _appSettingRepository = appSettingRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ServiceResponse<AppSettingDto>> Handle(GetAppSettingByKeyQuery request, CancellationToken cancellationToken)
        {
            var appsetting = await _appSettingRepository.FindBy(c => c.Key == request.Key).FirstOrDefaultAsync();
            if (appsetting == null)
            {
                _logger.LogError("AppSetting key is not available");
                return ServiceResponse<AppSettingDto>.Return404();
            }

            return ServiceResponse<AppSettingDto>.ReturnResultWith200(_mapper.Map<AppSettingDto>(appsetting));
        }
    }
}
