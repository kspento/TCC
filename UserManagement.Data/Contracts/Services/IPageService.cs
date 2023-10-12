using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Data.Dto.Page;
using UserManagement.Domain.Model.Page;
using UserManagement.Helper;

namespace UserManagement.Domain.Contracts.Services
{
    public interface IPageService
    {
        Task<ServiceResponse<PageDto>> AddPage(AddPageModel request);
        Task<ServiceResponse<PageDto>> DeletePage(DeletePageModel request);
        Task<List<PageDto>> GetAllPages();
        Task<ServiceResponse<PageDto>> GetPage(GetPageModel request);
        Task<ServiceResponse<PageDto>> UpdatePage(UpdatePageModel request);
    }
}