using System.Threading.Tasks;
using UserManagement.Data.Entities;
using UserManagement.Data.GenericRespository;
using UserManagement.Data.Repository.NLog;
using UserManagement.Data.Resources;

namespace UserManagement.Data.Repository.Contracts
{
    public interface INLogRespository : IGenericRepository<UserManagement.Data.Entities.NLog>
    {
        Task<NLogList> GetNLogsAsync(NLogResource nLogResource);
    }
}
