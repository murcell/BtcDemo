using AutoMapper;
using BtcDemo.Client.Models;

namespace BtcDemo.Client.Services;

public class CoinApiService: ICoinApiService
{
	private readonly HttpClient _httpClient;
	private IMapper _mapper;

	public CoinApiService(HttpClient httpClient, IMapper mapper)
	{
		_httpClient = httpClient;
		_mapper = mapper;
	}

	//public async Task<List<CoinModel>> GetBitcoinAsync(CancellationToken stoppingToken)
	//{
	//	return new List<CoinModel>();
	//}
}
