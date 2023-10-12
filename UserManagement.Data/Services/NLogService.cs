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

        public async Task<NLogList> GetNLogs(GetNLogsModel request, CancellationToken cancellationToken)
        {
            return await _nLogRespository.GetNLogsAsync(request.NLogResource);
        }

        public async Task<NLogDto> GetLog(GetNLogsModel request, CancellationToken cancellationToken)
        {
            var entity = await _nLogRespository.FindAsync(request.Id);
            if (entity != null)
                return _mapper.Map<NLogDto>(entity);
            //return ServiceResponse<NLogDto>.ReturnResultWith200(_mapper.Map<NLogDto>(entity));
            else
                return null;
            //return ServiceResponse<NLogDto>.Return404();
        }

        public async Task<ServiceResponse<NLogDto>> AddLog(AddLogModel request, CancellationToken cancellationToken)
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
            return ServiceResponse<NLogDto>.ReturnSuccess();
        }
    }
}
