using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using CurrencyTerminal.Domain.Entities;
using CurrencyTerminal.Domain.Interfaces;

namespace CurrencyTerminal.App.MapProfiles
{
    public class CurrencyDataProfile : Profile
    {
        public CurrencyDataProfile()
        {
            CreateMap<CurrencyData, CurrencyRate>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Vname))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.VchCode))
                .ForMember(dest => dest.RateToRuble, opt => opt.MapFrom(src => src.VunitRate))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Vname));
        }
    }
}
