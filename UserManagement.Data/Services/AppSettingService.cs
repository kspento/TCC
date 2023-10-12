using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Data.Context;
using UserManagement.Data.Dto.AppSetting;
using UserManagement.Data.Dto.User;
using UserManagement.Data.Entities;
using UserManagement.Data.Repository.Contracts;
using UserManagement.Data.UnitOfWork;
using UserManagement.Domain.Contracts.Services;
using UserManagement.Domain.Model.App;
using UserManagement.Helper;

namespace UserManagement.Domain.Services
{
    public class AppSettingService : IAppSettingService
    {
        private readonly IAppSettingRepository _appSettingRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAppSettingModel> _logger;
        private readonly UserInfoToken _userInfoToken;
        private readonly IUnitOfWork<UserContext> _uow;
        public AppSettingService(IAppSettingRepository appSettingRepository,
            IMapper mapper,
            ILogger<GetAppSettingModel> logger,
            UserInfoToken userInfoToken,
            IUnitOfWork<UserContext> uow
            )
        {
            _appSettingRepository = appSettingRepository;
            _mapper = mapper;
            _logger = logger;
            _userInfoToken = userInfoToken;
            _uow = uow;
        }
        public async Task<AppSettingDto> AddAppSetting(AddAppSettingModel request, CancellationToken cancellationToken)
        {
            var entityExist = await _appSettingRepository.FindBy(c => c.Key == request.Key).FirstOrDefaultAsync();
            if (entityExist != null)
            {
                _logger.LogError("AppSetting already exist.");
                //return ServiceResponse<AppSettingDto>.Return409("App Setting already exist.");
            }
            var entity = _mapper.Map<AppSetting>(request);
            entity.Id = Guid.NewGuid();
            entity.CreatedBy = Guid.Parse(_userInfoToken.Id);
            entity.CreatedDate = DateTime.Now.ToLocalTime();
            entity.ModifiedBy = Guid.Parse(_userInfoToken.Id);
            _appSettingRepository.Add(entity);
            //if (await _uow.SaveAsync() <= 0)
            //{
            //    return ServiceResponse<AppSettingDto>.Return500();
            //}
            var entityDto = _mapper.Map<AppSettingDto>(entity);

            return entityDto;
            //return ServiceResponse<AppSettingDto>.ReturnResultWith200(entityDto);
        }

        public async Task DeleteAppSetting(DeleteAppSettingModel request, CancellationToken cancellationToken)
        {
            var entityExist = await _appSettingRepository.FindAsync(request.Id);
            if (entityExist == null)
            {
                _logger.LogError("AppSetting Not Found.");
                //return ServiceResponse<AppSettingDto>.Return404();
            }
            entityExist.IsDeleted = true;
            entityExist.DeletedBy = Guid.Parse(_userInfoToken.Id);
            entityExist.DeletedDate = DateTime.Now.ToLocalTime();
            _appSettingRepository.Update(entityExist);
            //if (await _uow.SaveAsync() <= 0)
            //{
            //    return ServiceResponse<AppSettingDto>.Return500();
            //}

            //return entityExist;
            //return ServiceResponse<AppSettingDto>.ReturnResultWith204();
        }

        public async Task<List<AppSettingDto>> GetAllAppSetting(GetAppSettingModel request, CancellationToken cancellationToken)
        {

            var entities = await _appSettingRepository.All.ToListAsync();
            return _mapper.Map<List<AppSettingDto>>(entities);
            //return ServiceResponse<List<AppSettingDto>>.ReturnResultWith200(_mapper.Map<List<AppSettingDto>>(entities));
        }
        public async Task<ServiceResponse<AppSettingDto>> GetAppSettingByKey(GetAppSettingModel request, CancellationToken cancellationToken)
        {
            var appsetting = await _appSettingRepository.FindBy(c => c.Key == request.Key).FirstOrDefaultAsync();
            if (appsetting == null)
            {
                _logger.LogError("AppSetting key is not available");
                return ServiceResponse<AppSettingDto>.Return404();
            }

            return ServiceResponse<AppSettingDto>.ReturnResultWith200(_mapper.Map<AppSettingDto>(appsetting));
        }

        public async Task<AppSettingDto> GetAppSetting(GetAppSettingModel request, CancellationToken cancellationToken)
        {
            var appsetting = await _appSettingRepository.FindBy(c => c.Id == request.Id).FirstOrDefaultAsync();
            return _mapper.Map<AppSettingDto>(appsetting);
            //if (appsetting == null)
            //{
            //    _logger.LogError("AppSetting key is not available");
            //    return ServiceResponse<AppSettingDto>.Return404();
            //}
            //return ServiceResponse<AppSettingDto>.ReturnResultWith200(_mapper.Map<AppSettingDto>(appsetting));
        }

        public async Task<AppSettingDto> UpdateAppSetting(UpdateAppSettingModel request, CancellationToken cancellationToken)
        {
            var entityExist = await _appSettingRepository.FindBy(c => c.Key == request.Key && c.Id != request.Id).FirstOrDefaultAsync();
            if (entityExist != null)
            {
                _logger.LogError("AppSetting already exist.");
                //return ServiceResponse<AppSettingDto>.Return409("App Setting already exist.");
            }
            var entity = _mapper.Map<AppSetting>(request);
            entity.ModifiedBy = Guid.Parse(_userInfoToken.Id);
            _appSettingRepository.Update(entity);
            //if (await _uow.SaveAsync() <= 0)
            //{
            //    return ServiceResponse<AppSettingDto>.Return500();
            //}
            var entityDto = _mapper.Map<AppSettingDto>(entity);

            return entityDto;
            //return ServiceResponse<AppSettingDto>.ReturnResultWith200(entityDto);
        }
    }
}
