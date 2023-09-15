using BtcDemo.Core.DTOs;
using BtcDemo.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BtcDemo.API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthenticationService _authenticationService;
		public AuthController(IAuthenticationService authenticationService)
		{
			_authenticationService = authenticationService;
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginDto loginDto)
		{
			var result = await _authenticationService.CreateTokenAsync(loginDto);
			return Ok(result);
		}

		[HttpPost("createTokenByRefreshToken")]
		public async Task<IActionResult> CreateTokenByRefreshToken(RefreshTokenDto refreshTokenDto)
		{
			var result = await _authenticationService.CreateTokenByRefreshToken(refreshTokenDto.RefreshToken);
			return Ok(result);
		}

		[HttpPost("revokeRefreshToken")]
		public async Task<IActionResult> RevokeRefreshToken(RefreshTokenDto refreshTokenDto)
		{
			var result = await _authenticationService.RevokeRefreshToken(refreshTokenDto.RefreshToken);
			return Ok(result);
		}
	}
}
