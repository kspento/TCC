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
        Task<AppSettingDto> AddAppSetting(AddAppSettingModel request, CancellationToken cancellationToken);
        Task DeleteAppSetting(DeleteAppSettingModel request, CancellationToken cancellationToken);
        Task<List<AppSettingDto>> GetAllAppSetting(GetAppSettingModel request, CancellationToken cancellationToken);
        Task<AppSettingDto> GetAppSetting(GetAppSettingModel request, CancellationToken cancellationToken);
        Task<ServiceResponse<AppSettingDto>> GetAppSettingByKey(GetAppSettingModel request, CancellationToken cancellationToken);
        Task<AppSettingDto> UpdateAppSetting(UpdateAppSettingModel request, CancellationToken cancellationToken);
    }
}