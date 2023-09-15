using BtcDemo.Core.DTOs;
using BtcDemo.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BtcDemo.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUserService _usersService;

		public UsersController(IUserService userService)
		{
			_usersService = userService;
		}

		/// <summary>
		/// Şu an dışarıya açık kullanıcı oluşturma metodu
		/// </summary>
		/// <param name="createUserDto"></param>
		/// <returns></returns>
		[HttpPost("createUser")]
		public async Task<IActionResult> CreateUser(CreateAppUserDto createAppUserDto)
		{
			return Ok(await _usersService.CreateUserAsync(createAppUserDto));
		}

		/// <summary>
		/// Sadece login olan kullanıcı kendi bilgilerini görür.
		/// </summary>
		/// <returns></returns>
		//[Authorize(Roles = "Viewer")]
		[HttpGet("getUser")]
		public async Task<IActionResult> GetUser()
		{
			return Ok(await _usersService.GetUserByName(HttpContext.User.Identity.Name));
		}

		/// <summary>
		/// sadece admin görebilir.
		/// </summary>
		/// <returns></returns>
		//[Authorize(Roles = "Admin")] // admin rolü eklenmedi henüz
		[HttpGet("getAll")]
		public async Task<IActionResult> GetAllUser()
		{
			return Ok(await _usersService.GetAllUsers());
		}
	}
}
