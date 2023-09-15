using AutoMapper;
using BtcDemo.Core.ComplexTypes;
using BtcDemo.Core.DTOs;
using BtcDemo.Core.Entities;
using BtcDemo.Core.Services;
using BtcDemo.Core.UnitOfWorks;
using BtcDemo.Core.Utilities.Results;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BtcDemo.Service.Services;

public class AuthenticationService : ServiceBase, IAuthenticationService
{
	private readonly ITokenService _tokenService;
	private readonly UserManager<AppUser> _userManager;

	public AuthenticationService(ITokenService tokenService, UserManager<AppUser> userManager, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
	{
		_tokenService = tokenService;
		_userManager = userManager;

	}

	public async Task<IDataResult<TokenDto>> CreateTokenAsync(LoginDto loginDto)
	{
		if (loginDto == null) throw new ArgumentNullException(nameof(loginDto));
		var user = await _userManager.FindByEmailAsync(loginDto.Email);
		if (user == null) return new DataResult<TokenDto>(ResultStatus.Error, "Kullanıcı bulunamadı", null);

		if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
			return new DataResult<TokenDto>(ResultStatus.Error, "Kullanıcı bulunamadı", null);

		var token = _tokenService.CreateToken(user);

		// daha önce bir resfresh token kaydetmiş miyim ona bakıyorum.
		var userRefreshToken = await UnitOfWork.UserRefreshTokens.GetManyAsync(x => x.UserId == user.Id);
		if (userRefreshToken.SingleOrDefault() == null)
		{
			await UnitOfWork.UserRefreshTokens.AddAsync(new UserRefreshToken { UserId = user.Id, Code = token.RefreshToken, Expiration = token.RefreshTokenExpiration });
		}
		else
		{
			userRefreshToken.SingleOrDefault().Code = token.RefreshToken;
			userRefreshToken.SingleOrDefault().Expiration = token.RefreshTokenExpiration;

		}

		await UnitOfWork.CommitAsync();

		return new DataResult<TokenDto>(ResultStatus.Success, "Token oluşturuldu", token);

	}

	public async Task<IDataResult<TokenDto>> CreateTokenByRefreshToken(string refreshToken)
	{
		var existRefreshToken = await UnitOfWork.UserRefreshTokens.GetManyAsync(x => x.Code == refreshToken);

		if (existRefreshToken.SingleOrDefault() == null)
		{
			return new DataResult<TokenDto>(ResultStatus.Error, "Refresh Token bulunamadı", null);
		}

		var user = await _userManager.FindByIdAsync(existRefreshToken.SingleOrDefault().UserId);
		if (user == null)
		{
			return new DataResult<TokenDto>(ResultStatus.Error, "Kullanıcı bulunamadı", null);
		}

		var tokenDto = _tokenService.CreateToken(user);

		existRefreshToken.SingleOrDefault().Code = tokenDto.RefreshToken;
		existRefreshToken.SingleOrDefault().Expiration = tokenDto.RefreshTokenExpiration;

		await UnitOfWork.CommitAsync();
		return new DataResult<TokenDto>(ResultStatus.Success, "Token oluşturuldu", tokenDto);
	}

	public async Task<IResult> RevokeRefreshToken(string refreshToken)
	{
		var existRefreshToken = await UnitOfWork.UserRefreshTokens.GetManyAsync(x => x.Code == refreshToken);

		if (existRefreshToken.SingleOrDefault() == null)
		{
			return new Result(ResultStatus.Error, "Refresh Token bulunamadı");
		}

		await UnitOfWork.UserRefreshTokens.DeleteAsync(existRefreshToken.SingleOrDefault());
		await UnitOfWork.CommitAsync();
		return new Result(ResultStatus.Success);
	}
}
