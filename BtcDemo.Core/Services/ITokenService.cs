using BtcDemo.Core.DTOs;
using BtcDemo.Core.Entities;

namespace BtcDemo.Core.Services;

public interface ITokenService
{
	TokenDto CreateToken(AppUser user);
}
