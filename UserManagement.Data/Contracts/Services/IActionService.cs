using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Data.Dto.Action;
using UserManagement.Domain.Model.Action;

namespace UserManagement.Domain.Contracts.Services
{
    public interface IActionService
    {
        Task<ActionDto> AddAction(ActionModel request, CancellationToken cancellationToken);
        Task DeleteAction(ActionModel request, CancellationToken cancellationToken);
        Task<ActionDto> GetAction(ActionModel request, CancellationToken cancellationToken);
        Task<List<ActionDto>> GetAllAction(ActionModel request, CancellationToken cancellationToken);
        Task<ActionDto> UpdateAction(ActionModel request, CancellationToken cancellationToken);
    }
}