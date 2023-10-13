using AutoMapper;
using UserManagement.Data.Dto.Page;
using UserManagement.Data.Entities;
using UserManagement.Domain.Model.Page;

namespace UserManagement.API.Helpers.Mapping
{
    public class PageProfile : Profile
    {
        public PageProfile()
        {
            CreateMap<Page, PageDto>().ReverseMap();
            CreateMap<AddPageModel, Page>();
            CreateMap<UpdatePageModel, Page>();
        }
    }
}
