using AutoMapper;
using BtcDemo.API.Service.Model;
using BtcDemo.Core.DTOs;
using BtcDemo.Core.Services;
using BtcDemo.Core.UnitOfWorks;
using BtcDemo.Service.Services;

namespace BtcDemo.API.Service;

public class CoinloreService : ICoinloreService
{
	// IHttpClientFactory kullanmak istedim fakat bi türlü register edemedim..
	// o yüzden HttpClient kullandım.
	//private readonly IHttpClientFactory _httpClientFactory;

	//public CoinloreService(IHttpClientFactory httpClientFactory)
	//{
	//	_httpClientFactory = httpClientFactory;
	//}

	private readonly ICoinService _coinService;
	private readonly HttpClient _httpClient;
	private IMapper _mapper;

	public CoinloreService(ICoinService coinService, HttpClient httpClient, IMapper mapper)
	{
		_coinService = coinService;
		_httpClient = httpClient;
		_mapper = mapper;
	}

	public async Task<List<CoinloreDto>> GetBitcoinAsync(CancellationToken stoppingToken)
	{
		var response = await _httpClient.GetFromJsonAsync<List<CoinloreDto>>("ticker/?id=90");

		if (response!=null && response.Count>0)
		{
			var coin = _mapper.Map<AddCoinDto>(response.FirstOrDefault());

			// Burada 1 dakika içinde coin değerleri değişmiyordu
			// ben de değer değişiyorsa ekleme yaptırdım fyi..
			// ekleme yapmadan önce son eklenen bitcoinin değerini alıp gelen değerle
			// karşılaştırıp aynı ise ekletme

			if(!await _coinService.AnyAsync(c => c.PriceUsd == coin.PriceUsd))
				await _coinService.AddAsync(coin);
		}

		return response ?? new List<CoinloreDto>();
	}
	
}
