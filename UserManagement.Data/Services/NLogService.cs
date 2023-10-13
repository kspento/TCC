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

namespace UserManagement.Domain.Services
{
    public class NLogService : INLogService
    {
        private readonly INLogRespository _nLogRespository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<UserContext> _uow;
        public NLogService(INLogRespository nLogRespository, IMapper mapper, IUnitOfWork<UserContext> uow)
        {
            _nLogRespository = nLogRespository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<NLogList> GetNLogs(GetNLogsModel request)
        {
            return await _nLogRespository.GetNLogsAsync(request.NLogResource);
        }

        public async Task<NLogDto> GetLog(GetNLogsModel request)
        {
            var entity = await _nLogRespository.FindAsync(request.Id);
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
    }
}
