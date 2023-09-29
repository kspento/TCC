using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Data.Dto.AppSetting;
using UserManagement.Helper;
using UserManagement.MediatR.Queries;
using UserManagement.Repository;

namespace UserManagement.MediatR.Handlers
{
    public class GetAllAppSettingQueryHandler : IRequestHandler<GetAllAppSettingQuery, ServiceResponse<List<AppSettingDto>>>
    {
        private readonly IAppSettingRepository _appSettingRepository;
        private readonly IMapper _mapper;
        public GetAllAppSettingQueryHandler(
           IAppSettingRepository appSettingRepository,
            IMapper mapper
            )
        {
            _appSettingRepository = appSettingRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<AppSettingDto>>> Handle(GetAllAppSettingQuery request, CancellationToken cancellationToken)
        {
            
            var entities = await _appSettingRepository.All.ToListAsync();
            return ServiceResponse<List<AppSettingDto>>.ReturnResultWith200(_mapper.Map<List<AppSettingDto>>(entities));
        }
    }
}
