using AutoMapper;
using UserManagement.Data.Dto.EmailTemplate;
using UserManagement.Data.Entities;
using UserManagement.MediatR.Commands;

namespace UserManagement.API.Helpers
{
    public class EmailTemplateProfile : Profile
    {
        public EmailTemplateProfile()
        {
            CreateMap<EmailTemplateDto, EmailTemplate>().ReverseMap();
            CreateMap<AddEmailTemplateCommand, EmailTemplate>();
            CreateMap<UpdateEmailTemplateCommand, EmailTemplate>().ReverseMap();
        }
    }
}
