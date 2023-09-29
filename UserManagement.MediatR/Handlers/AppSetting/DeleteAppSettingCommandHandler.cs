using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Common.UnitOfWork;
using UserManagement.Data.Dto;
using UserManagement.Domain;
using UserManagement.Helper;
using UserManagement.MediatR.Commands;
using UserManagement.Repository;

namespace UserManagement.MediatR.Handlers
{
    public class DeleteAppSettingCommandHandler : IRequestHandler<DeleteAppSettingCommand, ServiceResponse<AppSettingDto>>
    {
        private readonly IAppSettingRepository _appSettingRepository;
        private readonly IUnitOfWork<UserContext> _uow;
        private readonly IMapper _mapper;
        private readonly UserInfoToken _userInfoToken;
        private readonly ILogger<DeleteAppSettingCommandHandler> _logger;
        public DeleteAppSettingCommandHandler(
           IAppSettingRepository appSettingRepository,
            IMapper mapper,
            IUnitOfWork<UserContext> uow,
            UserInfoToken userInfoToken,
            ILogger<DeleteAppSettingCommandHandler> logger
            )
        {
            _appSettingRepository = appSettingRepository;
            _mapper = mapper;
            _uow = uow;
            _userInfoToken = userInfoToken;
            _logger = logger;
        }

        public async Task<ServiceResponse<AppSettingDto>> Handle(DeleteAppSettingCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _appSettingRepository.FindAsync(request.Id);
            if (entityExist == null)
            {
                _logger.LogError("AppSetting Not Found.");
                return ServiceResponse<AppSettingDto>.Return404();
            }
            entityExist.IsDeleted = true;
            entityExist.DeletedBy = Guid.Parse(_userInfoToken.Id);
            entityExist.DeletedDate = DateTime.Now.ToLocalTime();
            _appSettingRepository.Update(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<AppSettingDto>.Return500();
            }
            return ServiceResponse<AppSettingDto>.ReturnResultWith204();
        }
    }
}
