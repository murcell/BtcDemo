using BtcDemo.API.Service.Model;
using BtcDemo.Core.DTOs;

namespace BtcDemo.API.Service
{
	public interface ICoinloreService
	{
		//Task<List<CoinloreDto>> GetCoin(CancellationToken stoppingToken);
		Task<List<CoinloreDto>> GetBitcoinAsync(CancellationToken stoppingToken);
	}
}
