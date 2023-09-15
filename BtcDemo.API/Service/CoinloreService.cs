using AutoMapper;
using BtcDemo.API.Service.Model;
using BtcDemo.Core.DTOs;
using BtcDemo.Core.Services;
using BtcDemo.Core.UnitOfWorks;

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

	private readonly IUnitOfWork _unitOfWork;
	private readonly ICoinService _coinService;
	private readonly HttpClient _httpClient;
	private IMapper _mapper;

	public CoinloreService(IUnitOfWork unitOfWork, ICoinService coinService, HttpClient httpClient, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
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

			// todo: ekleme yapmadan önce son eklenen bitcoinin değerini alıp gelen değerle
			// karşılaştırıp aynı ise ekletme

			await AddAsync(coin);
		}

		return response ?? new List<CoinloreDto>();
	}

	private async Task AddAsync(AddCoinDto coin)
	{
		await _coinService.AddAsync(coin);
	}

}
