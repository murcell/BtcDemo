using BtcDemo.Core.Configurations;
using BtcDemo.Core.DTOs;
using BtcDemo.Core.Entities;
using BtcDemo.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace BtcDemo.Service.Services;

public class TokenService : ITokenService
{

	private readonly UserManager<AppUser> _userManager;
	private readonly RoleManager<AppRole> _roleManager;
	private readonly CustomTokenOption _tokenOption;

	public TokenService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IOptions<CustomTokenOption> options)
	{
		_userManager = userManager;
		_roleManager = roleManager;
		_tokenOption = options.Value;
	}

	private string CreateRefreshToken()
	{
		var numberByte = new Byte[32];
		using var rnd = RandomNumberGenerator.Create();
		rnd.GetBytes(numberByte);
		return Convert.ToBase64String(numberByte);
	}

	private IEnumerable<Claim> GetClaims(AppUser userApp, List<string> audiences)
	{
		var roles = _userManager.GetRolesAsync(userApp).Result;

		var userList = new List<Claim> {

			new Claim(ClaimTypes.NameIdentifier,userApp.Id),
			new Claim(ClaimTypes.Name,userApp.UserName), 
                // şimdilik tek bir rolü olduğu için bu şekilde yaptım.
                new Claim(ClaimTypes.Role, roles[0]),
			new Claim(JwtRegisteredClaimNames.Email,userApp.Email),
			new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),

		};

		userList.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
		return userList;
	}

	public TokenDto CreateToken(AppUser userApp)
	{

		var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);
		var refreshTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.RefreshTokenExpiration);

		var securityKey = SignService.GetSymmetricSecurityKey(_tokenOption.SecurityKey);

		SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
		JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
			issuer: _tokenOption.Issuer,
			expires: accessTokenExpiration,
			notBefore: DateTime.Now,
			claims: GetClaims(userApp, _tokenOption.Audience),
			signingCredentials: signingCredentials);

		var handler = new JwtSecurityTokenHandler();
		var token = handler.WriteToken(jwtSecurityToken);

		var tokenDto = new TokenDto
		{
			AccessToken = token,
			RefreshToken = CreateRefreshToken(),
			AccessTokenExpiration = accessTokenExpiration,
			RefreshTokenExpiration = refreshTokenExpiration
		};

		return tokenDto;
	}
}
