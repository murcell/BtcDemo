using AutoMapper;
using BtcDemo.Client.Models;

namespace BtcDemo.Client.ProfileMap
{
	public class ModelProfile:Profile
	{
		public ModelProfile()
		{

			//CreateMap<CoinModel, CoinDto>().ReverseMap();
			//CreateMap<CoinDto, AddCoinDto>().ReverseMap();
			//CreateMap<CoinloreDto, AddCoinDto>().ReverseMap();
			//CreateMap<AddCoinDto, CoinloreDto>().ReverseMap();

			//CreateMap<AddCoinDto, CoinloreDto>()
			//	.ForMember(destinationMember: c => c.symbol, memberOptions: opt => opt.MapFrom(c => c.Symbol))
			//	.ForMember(destinationMember: c => c.name, memberOptions: opt => opt.MapFrom(c => c.Name))
			//	.ForMember(destinationMember: c => c.rank, memberOptions: opt => opt.MapFrom(c => c.Rank))
			//	.ForMember(destinationMember: c => c.price_usd, memberOptions: opt => opt.MapFrom(c => c.PriceUsd))
			//	.ForMember(destinationMember: c => c.percent_change_24h, memberOptions: opt => opt.MapFrom(c => c.PercentChange24h))
			//	.ForMember(destinationMember: c => c.percent_change_1h, memberOptions: opt => opt.MapFrom(c => c.PercentChange1h))
			//	.ForMember(destinationMember: c => c.percent_change_7d, memberOptions: opt => opt.MapFrom(c => c.PercentChange7d))
			//	.ForMember(destinationMember: c => c.csupply, memberOptions: opt => opt.MapFrom(c => c.CSupply))
			//	.ForMember(destinationMember: c => c.tsupply, memberOptions: opt => opt.MapFrom(c => c.TSupply))
			//	.ForMember(destinationMember: c => c.msupply, memberOptions: opt => opt.MapFrom(c => c.MSupply))
			//	.ReverseMap();

			//CreateMap<CoinloreDto, AddCoinDto>()
			//	.ForMember(destinationMember: c => c.Symbol, memberOptions: opt => opt.MapFrom(c => c.symbol))
			//	.ForMember(destinationMember: c => c.Name, memberOptions: opt => opt.MapFrom(c => c.name))
			//	.ForMember(destinationMember: c => c.Rank, memberOptions: opt => opt.MapFrom(c => c.rank))
			//	.ForMember(destinationMember: c => c.PriceUsd, memberOptions: opt => opt.MapFrom(c => c.price_usd))
			//	.ForMember(destinationMember: c => c.PercentChange24h, memberOptions: opt => opt.MapFrom(c => c.percent_change_24h))
			//	.ForMember(destinationMember: c => c.PercentChange1h, memberOptions: opt => opt.MapFrom(c => c.percent_change_1h))
			//	.ForMember(destinationMember: c => c.PercentChange7d, memberOptions: opt => opt.MapFrom(c => c.percent_change_7d))
			//	.ForMember(destinationMember: c => c.CSupply, memberOptions: opt => opt.MapFrom(c => c.csupply))
			//	.ForMember(destinationMember: c => c.TSupply, memberOptions: opt => opt.MapFrom(c => c.tsupply))
			//	.ForMember(destinationMember: c => c.MSupply, memberOptions: opt => opt.MapFrom(c => c.msupply))
			//	.ReverseMap();
		}
	}
}

