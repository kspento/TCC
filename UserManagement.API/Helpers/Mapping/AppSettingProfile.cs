using AutoMapper;
using UserManagement.Data.Dto.AppSetting;
using UserManagement.Data.Entities;
using UserManagement.Domain.Model.App;

namespace UserManagement.API.Helpers.Mapping
{
    public class AppSettingProfile : Profile
    {
        public AppSettingProfile()
        {
            CreateMap<AppSettingDto, AppSetting>().ReverseMap();
            CreateMap<AddAppSettingModel, AppSetting>();
            CreateMap<UpdateAppSettingModel, AppSetting>().ReverseMap();
        }
    }
}
