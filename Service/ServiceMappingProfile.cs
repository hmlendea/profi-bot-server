using System;
using AutoMapper;

using ProfiBotServer.Api.Requests;
using ProfiBotServer.DataAccess.DataObjects;
using ProfiBotServer.Service.Models;

namespace ProfiBotServer.Service
{
    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            CreateMap<RecordPrizeRequest, Prize>()
                .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => DateTime.Parse(src.Timestamp)))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserPhoneNumber));

            CreateMap<PrizeEntity, Prize>();
            CreateMap<UserEntity, User>();
        }
    }
}
