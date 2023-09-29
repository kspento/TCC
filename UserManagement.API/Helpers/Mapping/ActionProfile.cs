using AutoMapper;
using UserManagement.Data.Dto.Action;
using UserManagement.Data.Entities;
using UserManagement.MediatR.Commands;

namespace UserManagement.API.Helpers.Mapping
{
    public class ActionProfile : Profile
    {
        public ActionProfile()
        {
            CreateMap<Action, ActionDto>().ReverseMap();
            CreateMap<AddActionCommand, Action>();
            CreateMap<UpdateActionCommand, Action>();
        }
    }
}
