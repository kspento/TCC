using AutoMapper;
using UserManagement.Data.Dto.PageAction;
using UserManagement.Data.Entities;
using UserManagement.Domain.Model.PageAction;

namespace UserManagement.API.Helpers.Mapping
{
    public class PageActionProfile : Profile
    {
        public PageActionProfile()
        {
            CreateMap<PageAction, PageActionDto>().ReverseMap();
            CreateMap<AddPageActionModel, PageAction>().ReverseMap();
        }
    }
}
