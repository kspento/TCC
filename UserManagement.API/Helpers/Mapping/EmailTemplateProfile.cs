using AutoMapper;
using UserManagement.Data.Dto.EmailTemplate;
using UserManagement.Data.Entities;

namespace UserManagement.API.Helpers
{
    public class EmailTemplateProfile : Profile
    {
        public EmailTemplateProfile()
        {
            CreateMap<EmailTemplateDto, EmailTemplate>().ReverseMap();
            //TODO VER ESSE MAPP AQUI
            //CreateMap<AddEmailTemplateCommand, EmailTemplate>();
            //CreateMap<UpdateEmailTemplateCommand, EmailTemplate>().ReverseMap();
        }
    }
}
