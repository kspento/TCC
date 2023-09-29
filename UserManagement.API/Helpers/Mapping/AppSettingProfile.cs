using AutoMapper;
using UserManagement.Data.Dto.AppSetting;
using UserManagement.Data.Entities;
using UserManagement.MediatR.Commands;

namespace UserManagement.API.Helpers.Mapping
{
    public class AppSettingProfile : Profile
    {
        public AppSettingProfile()
        {
            CreateMap<AppSettingDto, AppSetting>().ReverseMap();
            CreateMap<AddAppSettingCommand, AppSetting>();
            CreateMap<UpdateAppSettingCommand, AppSetting>().ReverseMap();

        }
    }
}
