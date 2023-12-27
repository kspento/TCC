using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Data.Dto.Action;
using UserManagement.Domain.Model.Action;

namespace UserManagement.Domain.Contracts.Services
{
    public interface IActionService
    {
        Task<ActionDto> AddAction(ActionModel request);
        Task DeleteAction(ActionModel request);
        Task<ActionDto> GetAction(ActionModel request);
        Task<List<ActionDto>> GetAllAction();
        Task<ActionDto> UpdateAction(ActionModel request);
    }
}