using AutoMapper;
using UserManagement.Data.Dto.Page;
using UserManagement.Data.Entities;
using UserManagement.MediatR.Commands;

namespace UserManagement.API.Helpers.Mapping
{
    public class PageProfile : Profile
    {
        public PageProfile()
        {
            CreateMap<Page, PageDto>().ReverseMap();
            CreateMap<AddPageCommand, Page>();
            CreateMap<UpdatePageCommand, Page>();
        }
    }
}
