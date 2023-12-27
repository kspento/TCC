using System.Threading.Tasks;
using System.Threading;
using UserManagement.Data.Repository.NLog;
using UserManagement.Data.Repository.Contracts;
using UserManagement.Data.Dto.NLog;
using UserManagement.Helper;
using AutoMapper;
using System;
using UserManagement.Domain.Model.NLog;
using UserManagement.Data.Context;
using UserManagement.Data.UnitOfWork;
using UserManagement.Domain.Contracts.Services;
using UserManagement.Domain.Exception;
using UserManagement.Data.Dto.User;
using UserManagement.Data.Entities;
using UserManagement.Data.Resources;
using UserManagement.Repository;
using System.Runtime.CompilerServices;
using UserManagement.Data.PropertyMapping;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace UserManagement.Domain.Services
{
    public class NLogService : INLogService
    {
        private readonly INLogRespository _nLogRespository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<UserContext> _uow;
        private readonly IUserRepository _userRepository;
        private readonly IPropertyMappingService _propertyMappingService;
        public NLogService(INLogRespository nLogRespository, IMapper mapper, IUnitOfWork<UserContext> uow, IUserRepository userRepository, IPropertyMappingService propertyMappingService)
        {
            _nLogRespository = nLogRespository;
            _mapper = mapper;
            _uow = uow;
            _userRepository = userRepository;
            _propertyMappingService = propertyMappingService;
        }

        public async Task<NLogList> GetNLogs(GetNLogsModel request)
        {
            return await _nLogRespository.GetNLogsAsync(request.NLogResource);
        }

        public async Task<NLogDto> GetLogById(Guid id)
        {
            var entity = await _nLogRespository.FindAsync(id);
            if (entity != null)
                return _mapper.Map<NLogDto>(entity);
            else
                throw new NotFoundException(string.Empty);
        }

        public async Task AddLog(AddLogModel request)
        {
            _nLogRespository.Add(new Data.Entities.NLog
            {
                Id = Guid.NewGuid(),
                Logged = DateTime.Now.ToLocalTime(),
                Level = "Error",
                Message = request.ErrorMessage,
                Source = "Angular",
                Exception = request.Stack
            });
            await _uow.SaveAsync();
        }

        public async Task<NLogList> GetLogs(NLogResource nLogResource)
        {
           var result = await _nLogRespository.GetNLogsAsync(nLogResource);

            return result;
        }


    }
}
