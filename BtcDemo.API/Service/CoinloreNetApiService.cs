using BtcDemo.API.Service.Model;
using System.Net.Http;

namespace BtcDemo.API.Service
{
	public class CoinloreNetApiService: ICoinloreNetApiService
	{
		private readonly HttpClient _httpClient;

		public CoinloreNetApiService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<List<CoinloreDto>> GetBitcoinAsync()
		{
			var response = await _httpClient.GetFromJsonAsync<List<CoinloreDto>>("ticker/?id=90");
			return response ?? new List<CoinloreDto>();
		}

	}
}
