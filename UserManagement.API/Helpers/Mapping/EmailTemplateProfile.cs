using AutoMapper;
using UserManagement.Data.Dto.EmailTemplate;
using UserManagement.Data.Entities;
using UserManagement.Domain.Model.EmailTemplate;

namespace UserManagement.API.Helpers
{
    public class EmailTemplateProfile : Profile
    {
        public EmailTemplateProfile()
        {
            CreateMap<EmailTemplateDto, EmailTemplate>().ReverseMap();
            CreateMap<AddEmailTemplateModel, EmailTemplate>();
            CreateMap<UpdateEmailTemplateModel, EmailTemplate>().ReverseMap();
        }
    }
}
