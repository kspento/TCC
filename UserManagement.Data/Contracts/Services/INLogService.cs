using System.Threading;
using System.Threading.Tasks;
using UserManagement.Data.Dto.NLog;
using UserManagement.Data.Repository.NLog;
using UserManagement.Domain.Model.NLog;
using UserManagement.Helper;

namespace UserManagement.Domain.Contracts.Services
{
    public interface INLogService
    {
        Task<ServiceResponse<NLogDto>> AddLog(AddLogModel request, CancellationToken cancellationToken);
        Task<NLogDto> GetLog(GetNLogsModel request, CancellationToken cancellationToken);
        Task<NLogList> GetNLogs(GetNLogsModel request, CancellationToken cancellationToken);
    }
}