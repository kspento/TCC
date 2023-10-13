using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Data.Dto.Page;
using UserManagement.Domain.Model.Page;

namespace UserManagement.Domain.Contracts.Services
{
    public interface IPageService
    {
        Task<PageDto> AddPage(AddPageModel request);
        Task DeletePage(DeletePageModel request);
        Task<List<PageDto>> GetAllPages();
        Task<PageDto> GetPage(GetPageModel request);
        Task<PageDto> UpdatePage(UpdatePageModel request);
    }
}