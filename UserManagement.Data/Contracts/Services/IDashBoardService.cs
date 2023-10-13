using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Data.Dto.User;

namespace UserManagement.Domain.Contracts.Services
{
    public interface IDashBoardService
    {
        Task<int> GetActiveUserCount(CancellationToken cancellationToken);
        Task<int> GetInactiveUserCount(CancellationToken cancellationToken);
        Task<List<UserDto>> GetOnlineUsers(List<UserDto> request);
    }
}