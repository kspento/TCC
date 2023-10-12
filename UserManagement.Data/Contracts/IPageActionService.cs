using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Data.Dto.PageAction;
using UserManagement.Domain.Model.PageAction;
using UserManagement.MediatR.Queries;

public interface IPageActionService
{
    Task<PageActionDto> AddPageAction(AddPageActionModel request);
    Task DeletePageAction(DeletePageActionModel request);
    Task<List<PageActionDto>> GetAllPageActions();
    Task<PageActionDto> GetPageAction(GetPageActionModel request);
}