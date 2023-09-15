using AutoMapper;
using BtcDemo.Service.AutoMapper.Profiles;

namespace BtcDemo.Service.AutoMapper.Mapper;

public static class ObjectMapper
{
	private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
	{
		var config = new MapperConfiguration(cfg =>
		{
			cfg.AddProfile<CoinProfile>();
			
			cfg.AddProfile<AppUserProfile>();
		});

		return config.CreateMapper();

	});

	public static IMapper Mapper => lazy.Value;
}
