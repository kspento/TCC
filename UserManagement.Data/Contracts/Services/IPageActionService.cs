using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Data.Dto.PageAction;
using UserManagement.Domain.Model.PageAction;

namespace UserManagement.Domain.Contracts.Services
{
    public interface IPageActionService
    {
        Task<PageActionDto> AddPageAction(AddPageActionModel request);
        Task DeletePageAction(Guid id);
        Task<List<PageActionDto>> GetAllPageActions();
        Task<PageActionDto> GetPageAction(GetPageActionModel request);
    }
}