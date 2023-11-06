using System;
using System.Threading.Tasks;
using UserManagement.Data.Dto.NLog;
using UserManagement.Data.Repository.NLog;
using UserManagement.Data.Resources;
using UserManagement.Domain.Model.NLog;
using UserManagement.Repository;

namespace UserManagement.Domain.Contracts.Services
{
    public interface INLogService
    {
        Task AddLog(AddLogModel request);
        Task<NLogDto> GetLogById(Guid id);
        Task<NLogList> GetNLogs(GetNLogsModel request);
        Task<NLogList> GetLogs(NLogResource logResource);
    }
}