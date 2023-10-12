using AutoMapper;
using UserManagement.Data.Dto.Email;
using UserManagement.Data.Entities;
using UserManagement.Domain.Model.Email;
using UserManagement.MediatR.Commands;

namespace UserManagement.API.Helpers.Mapping
{
    public class EmailProfile : Profile
    {
        public EmailProfile()
        {
            CreateMap<EmailSMTPSetting, EmailSMTPSettingDto>().ReverseMap();
            CreateMap<EmailSMTPSetting, EmailSettingModel>().ReverseMap();
            //CreateMap<EmailSMTPSetting, UpdateEmailSMTPSettingCommand>().ReverseMap();
        }
    }
}
