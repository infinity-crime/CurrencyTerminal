using AutoMapper;
using CurrencyTerminal.App.DTOs;
using CurrencyTerminal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyTerminal.App.MapProfiles
{
    public class CurrencyRateProfile : Profile
    {
        public CurrencyRateProfile()
        {
            CreateMap<CurrencyRate, CurrencyRateDto>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.RateInRubles, opt => opt.MapFrom(src => src.RateToRuble))
                .ForMember(dest => dest.PreviousRateValue, opt => opt.MapFrom(src => src.PreviousValueRate));
        }
    }
}
