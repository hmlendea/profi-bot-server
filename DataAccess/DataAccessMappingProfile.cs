using AutoMapper;

using ProfiBotServer.DataAccess.DataObjects;
using ProfiBotServer.Service.Models;

namespace ProfiBotServer.DataAccess
{
    public class DataAccessMappingProfile : Profile
    {
        public DataAccessMappingProfile()
        {
            CreateMap<Prize, PrizeEntity>()
                .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => src.Timestamp.ToString("o")));
            CreateMap<User, UserEntity>();
        }
    }
}
