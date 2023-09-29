using AutoMapper;
using UserManagement.Data.Dto.NLog;

namespace UserManagement.API.Helpers.Mapping
{
    public class NLogProfile : Profile
    {
        public NLogProfile()
        {
            CreateMap<Data.Entities.NLog, NLogDto>().ReverseMap();
        }
    }
}
