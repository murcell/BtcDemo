using AutoMapper;
using BtcDemo.API.Service;
using BtcDemo.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BtcDemo.API.Controllers
{
	//[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class CoinsController : ControllerBase
	{
		private readonly ICoinService _coinService;
		private readonly CoinloreNetApiService _coinloreNetApiService;
		IMapper _mapper;

		public CoinsController(ICoinService coinService, IMapper mapper, CoinloreNetApiService coinloreNetApiService)
		{
			_coinService = coinService;
			_mapper = mapper;
			_coinloreNetApiService = coinloreNetApiService;
		}

		//[Authorize(Roles = "Viewer")]
		[HttpGet("getAllCoins")]
		public async Task<IActionResult> GetCoins()
		{
			return Ok(await _coinService.GetManyAsync(x => x.IsDeleted == false));
		}

		[HttpGet("getAllCoins2")]
		public async Task<IActionResult> GetCoins2()
		{
			var test = await _coinloreNetApiService.GetBitcoinAsync();

			return Ok(await _coinService.GetManyAsync(x => x.IsDeleted == false));
		}

		//[Authorize(Roles = "Viewer")]
		[HttpGet("getCoinsByLastOneHour")]
		public async Task<IActionResult> GetCoinsByLastOneHour()
		{
			return Ok(await _coinService.GetManyAsync(x => x.IsDeleted == false && x.CreatedDate>DateTime.Now.AddHours(-1)));
		}

		//[Authorize(Roles = "Viewer")]
		[HttpGet("getCoinsByLastFiveHours")]
		public async Task<IActionResult> GetCoinsByLastFiveHours()
		{
			return Ok(await _coinService.GetManyAsync(x => x.IsDeleted == false && x.CreatedDate > DateTime.Now.AddHours(-5)));
		}

		//[Authorize(Roles = "Viewer")]
		[HttpGet("getCoinsByLastSevenDays")]
		public async Task<IActionResult> GetCoinsByLastSevenDays()
		{
			return Ok(await _coinService.GetManyAsync(x => x.IsDeleted == false && x.CreatedDate > DateTime.Now.AddDays(-7)));
		}

		//[Authorize(Roles = "Viewer")]
		[HttpGet("getCoinsByLastOneMonth")]
		public async Task<IActionResult> getCoinsByLastOneMonth()
		{
			return Ok(await _coinService.GetManyAsync(x => x.IsDeleted == false && x.CreatedDate > DateTime.Now.AddMonths(-1)));
		}
	}
}
