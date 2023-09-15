using BtcDemo.API.Service.Model;

namespace BtcDemo.API.Service
{
	public interface ICoinloreNetApiService
	{
		Task<List<CoinloreDto>> GetBitcoinAsync();
	}
}
