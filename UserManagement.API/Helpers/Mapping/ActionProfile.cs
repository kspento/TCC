using AutoMapper;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using UserManagement.Data.Dto.Action;
using UserManagement.Data.Entities;

namespace UserManagement.API.Helpers.Mapping
{
    public class ActionProfile : Profile
    {
        public ActionProfile()
        {
            CreateMap<Action, ActionDto>().ReverseMap();
            CreateMap<ActionModel, Action>();
            //CreateMap<UpdateActionCommand, Action>();
        }
    }
}
