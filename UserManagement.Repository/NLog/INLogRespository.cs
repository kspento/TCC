using System.Threading.Tasks;
using UserManagement.Data.Entities;
using UserManagement.Data.GenericRespository;
using UserManagement.Data.Resources;

namespace UserManagement.Repository
{
    public interface INLogRespository : IGenericRepository<NLog>
    {
        Task<NLogList> GetNLogsAsync(NLogResource nLogResource);
    }
}
