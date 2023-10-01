using AutoMapper;
using BtcDemo.Core.Services;
using Microsoft.AspNetCore.Authorization;
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

		[HttpGet("getAllCoins")]
		public async Task<IActionResult> GetCoins()
		{
			return Ok(await _coinService.GetManyAsync(x => x.IsDeleted == false));
		}

        [HttpGet("getCoinsByFilter/{dayFilter}")]
        public async Task<IActionResult> GetCoinsByFilter(int dayFilter)
        {
            return Ok(await _coinService.GetManyAsync(x => x.IsDeleted == false &&  x.CreatedDate > DateTime.Now.AddHours(-dayFilter)));
        }

        [HttpGet("getCoinsByLastOneMonth")]
        public async Task<IActionResult> GetCoinsByLastOneMonth()
        {
            return Ok(await _coinService.GetManyAsync(x => x.IsDeleted == false && x.CreatedDate > DateTime.Now.AddMonths(-1)));
        }

        [HttpGet("getCoinsByLastSevenDays")]
        public async Task<IActionResult> GetCoinsByLastSevenDays()
        {
            return Ok(await _coinService.GetManyAsync(x => x.IsDeleted == false && x.CreatedDate > DateTime.Now.AddDays(-7)));
        }
        
    }
}
