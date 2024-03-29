using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // TODO
            // AppUser MemberDto

            CreateMap<RegisterDto, AppUser>();
            CreateMap<Property, PropertyDto>();
            CreateMap<AppUser, AppUserDto>();
            CreateMap<AddElectricityDto, ElectricityPurchase>();
            CreateMap<ElectricityPurchase, ElectricityPurchaseDto>();
        }
    }
}