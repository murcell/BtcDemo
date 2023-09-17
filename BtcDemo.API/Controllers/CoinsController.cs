using AutoMapper;
using BtcDemo.API.Service;
using BtcDemo.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BtcDemo.API.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class CoinsController : ControllerBase
	{
		private readonly ICoinService _coinService;
		IMapper _mapper;

		public CoinsController(ICoinService coinService, IMapper mapper)
		{
			_coinService = coinService;
			_mapper = mapper;
	
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
			
			return Ok(await _coinService.GetManyAsync(x => x.IsDeleted == false));
		}

		//[Authorize(Roles = "Viewer")]
		[HttpGet("getCoinsByLastOneHour")]
		public async Task<IActionResult> GetCoinsByLastOneHour()
		{
			return Ok(await _coinService.GetManyAsync(x => x.IsDeleted == false && x.CreatedDate>DateTime.Now.AddHours(-1)));
		}

        [HttpGet("getCoinsByLastThreeHours")]
        public async Task<IActionResult> GetCoinsByLastThreeHours()
        {
            return Ok(await _coinService.GetManyAsync(x => x.IsDeleted == false && x.CreatedDate > DateTime.Now.AddHours(-3)));
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

        [HttpGet("getCoinsByLastFourHours/{dayFilter}")]
        public async Task<IActionResult> GetCoinsByLastFourHours(int dayFilter)
        {
            return Ok(await _coinService.GetManyAsync(x => x.IsDeleted == false && x.CreatedDate > DateTime.Now.AddHours(-5)));
        }
    }
}
