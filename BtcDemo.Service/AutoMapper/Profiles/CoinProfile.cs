using AutoMapper;
using BtcDemo.Core.DTOs;
using BtcDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BtcDemo.Service.AutoMapper.Profiles;

public class CoinProfile : Profile
{
    public CoinProfile()
    {
		//CreateMap<AddCoinDto, Coin>().ForMember(dest => dest.RegisterDate, opt => opt.MapFrom(x => DateTime.Now));

		CreateMap<CoinDto, Coin>().ReverseMap();
		CreateMap<Coin, CoinDto>().ReverseMap();
		CreateMap<AddCoinDto, Coin>().ReverseMap();
		CreateMap<Coin, AddCoinDto>().ReverseMap();
	}
}
