using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Data.Dto.AppSetting;
using UserManagement.Domain.Model.App;
using UserManagement.Helper;

namespace UserManagement.Domain.Contracts.Services
{
    public interface IAppSettingService
    {
        Task<AppSettingDto> AddAppSetting(AddAppSettingModel request);
        Task DeleteAppSetting(DeleteAppSettingModel request);
        Task<List<AppSettingDto>> GetAllAppSetting();
        Task<AppSettingDto> GetAppSetting(GetAppSettingModel request);
        Task<AppSettingDto> GetAppSettingByKey(GetAppSettingModel request);
        Task<AppSettingDto> UpdateAppSetting(UpdateAppSettingModel request);
    }
}