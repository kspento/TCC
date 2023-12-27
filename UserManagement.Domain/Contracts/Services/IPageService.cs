using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Data.Dto.Page;
using UserManagement.Domain.Model.Page;

namespace UserManagement.Domain.Contracts.Services
{
    public interface IPageService
    {
        Task<PageDto> AddPage(AddPageModel request);
        Task DeletePage(Guid id);
        Task<List<PageDto>> GetAllPages();
        Task<PageDto> GetPage(Guid id);
        Task<PageDto> UpdatePage(UpdatePageModel request);
    }
}