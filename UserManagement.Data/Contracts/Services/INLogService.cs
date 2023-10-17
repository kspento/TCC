using System.Threading.Tasks;
using UserManagement.Data.Dto.NLog;
using UserManagement.Data.Repository.NLog;
using UserManagement.Domain.Model.NLog;

namespace UserManagement.Domain.Contracts.Services
{
    public interface INLogService
    {
        Task AddLog(AddLogModel request);
        Task<NLogDto> GetLog(GetNLogsModel request);
        Task<NLogList> GetNLogs(GetNLogsModel request);
    }
}