using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Data.Context;
using UserManagement.Data.Dto.AppSetting;
using UserManagement.Data.Dto.User;
using UserManagement.Data.Entities;
using UserManagement.Data.Repository.Contracts;
using UserManagement.Data.UnitOfWork;
using UserManagement.Domain.Contracts.Services;
using UserManagement.Domain.Exception;
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
        public async Task<AppSettingDto> AddAppSetting(AddAppSettingModel request)
        {
            var entityExist = await _appSettingRepository.FindBy(c => c.Key == request.Key).FirstOrDefaultAsync();
            if (entityExist != null)
            {
                _logger.LogError("AppSetting already exist.");

                throw new AlreadyExistsException("App Setting already exist.");
            }
            var entity = _mapper.Map<AppSetting>(request);
            entity.Id = Guid.NewGuid();
            entity.CreatedBy = Guid.Parse(_userInfoToken.Id);
            entity.CreatedDate = DateTime.Now.ToLocalTime();
            entity.ModifiedBy = Guid.Parse(_userInfoToken.Id);
            _appSettingRepository.Add(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                throw new System.Exception();
            }
            var entityDto = _mapper.Map<AppSettingDto>(entity);

            return entityDto;
        }

        public async Task DeleteAppSetting(DeleteAppSettingModel request)
        {
            var entityExist = await _appSettingRepository.FindAsync(request.Id);
            if (entityExist == null)
            {
                _logger.LogError("AppSetting Not Found.");

                throw new NotFoundException("AppSetting Not Found.");
            }
            entityExist.IsDeleted = true;
            entityExist.DeletedBy = Guid.Parse(_userInfoToken.Id);
            entityExist.DeletedDate = DateTime.Now.ToLocalTime();
            _appSettingRepository.Update(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                throw new System.Exception();
            }
        }

        public async Task<List<AppSettingDto>> GetAllAppSetting(GetAppSettingModel request)
        {

            var entities = await _appSettingRepository.All.ToListAsync();
            return _mapper.Map<List<AppSettingDto>>(entities);
        }
        public async Task<AppSettingDto> GetAppSettingByKey(GetAppSettingModel request)
        {
            var appsetting = await _appSettingRepository.FindBy(c => c.Key == request.Key).FirstOrDefaultAsync();
            if (appsetting == null)
            {
                _logger.LogError("AppSetting key is not available");

                throw new NotFoundException("AppSetting key is not available");
            }

            return _mapper.Map<AppSettingDto>(appsetting);
        }

        public async Task<AppSettingDto> GetAppSetting(GetAppSettingModel request)
        {
            var appsetting = await _appSettingRepository.FindBy(c => c.Id == request.Id).FirstOrDefaultAsync();

            if (appsetting == null)
            {
                _logger.LogError("AppSetting key is not available");

                throw new NotFoundException("AppSetting key is not available");
            }
            return _mapper.Map<AppSettingDto>(appsetting);
        }

        public async Task<AppSettingDto> UpdateAppSetting(UpdateAppSettingModel request)
        {
            var entityExist = await _appSettingRepository.FindBy(c => c.Key == request.Key && c.Id != request.Id).FirstOrDefaultAsync();
            if (entityExist != null)
            {
                _logger.LogError("AppSetting already exist.");

                throw new AlreadyExistsException("App Setting already exist.");
            }
            var entity = _mapper.Map<AppSetting>(request);
            entity.ModifiedBy = Guid.Parse(_userInfoToken.Id);
            _appSettingRepository.Update(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                throw new System.Exception();
            }
            var entityDto = _mapper.Map<AppSettingDto>(entity);

            return entityDto;
        }
    }
}
