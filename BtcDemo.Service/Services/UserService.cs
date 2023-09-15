using AutoMapper;
using BtcDemo.Core.ComplexTypes;
using BtcDemo.Core.DTOs;
using BtcDemo.Core.Entities;
using BtcDemo.Core.Services;
using BtcDemo.Core.Utilities.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BtcDemo.Service.Services;

public class UserService : IUserService
{
	private readonly UserManager<AppUser> _userManager;
	private readonly IMapper _mapper;
	public UserService(UserManager<AppUser> userManager, IMapper mapper)
	{
		_userManager = userManager;
		_mapper = mapper;
	}

	public async Task<IDataResult<AppUserDto>> CreateUserAsync(CreateAppUserDto createUserDto)
	{
		var user = new AppUser
		{
			Email = createUserDto.Email,
			UserName = createUserDto.UserName
		};

		IdentityResult result = await _userManager.CreateAsync(user, createUserDto.Password);

		if (!result.Succeeded)
		{
			var errors = result.Errors.Select(x => x.Description).ToList();

			//return Response<AppUserDto>.Fail(new ErrorDto(errors, true), 400);
			return new DataResult<AppUserDto>(ResultStatus.Error, "Kullanıcı bulunamadı", null);
		}

		// Todo:burayı şimdilik işlemler uzamasın diye bu şekilde yaptım. 
		// Normalde admin paneli üzerinden kullanıcıya rolü eklerim.
		
		IdentityResult resultRole = await _userManager.AddToRoleAsync(user, "VIEWER");
		if (!resultRole.Succeeded)
		{
			var errors = result.Errors.Select(x => x.Description).ToList();
			return new DataResult<AppUserDto>(ResultStatus.Error, "Hata oluştu", null);
		}
		return new DataResult<AppUserDto>(ResultStatus.Success, "Kullanıcı oluşturuldu", _mapper.Map<AppUserDto>(user));
	}

	public async Task<IDataResult<IList<AppUserDto>>> GetAllUsers()
	{
		var users = await _userManager.Users.ToListAsync();
		if (users.Count == 0)
		{
			return new DataResult<IList<AppUserDto>>(ResultStatus.Error, "Kullanıcı bulunamadı", null);
		}
		return new DataResult<IList<AppUserDto>>(ResultStatus.Success, _mapper.Map<IList<AppUserDto>>(users));
	}

	public async Task<IDataResult<AppUserDto>> GetUserByEmail(string email)
	{
		var user = await _userManager.FindByEmailAsync(email);
		if (user == null)
		{
			return new DataResult<AppUserDto>(ResultStatus.Error, "Kullanıcı bulunamadı", null);
		}
		return new DataResult<AppUserDto>(ResultStatus.Success, _mapper.Map<AppUserDto>(user));
	}

	public async Task<IDataResult<AppUserDto>> GetUserByName(string userName)
	{
		var user = await _userManager.FindByNameAsync(userName);
		if (user == null)
		{
			return new DataResult<AppUserDto>(ResultStatus.Error, "Kullanıcı bulunamadı", null);
		}
		return new DataResult<AppUserDto>(ResultStatus.Success, _mapper.Map<AppUserDto>(user));
	}
}
