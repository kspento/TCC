﻿using AutoMapper;
using UserManagement.Data.Dto.PageAction;
using UserManagement.Data.Entities;
using UserManagement.MediatR.Commands;

namespace UserManagement.API.Helpers.Mapping
{
    public class PageActionProfile : Profile
    {
        public PageActionProfile()
        {
            CreateMap<PageAction, PageActionDto>().ReverseMap();
            CreateMap<AddPageActionCommand, PageAction>().ReverseMap();
        }
    }
}
