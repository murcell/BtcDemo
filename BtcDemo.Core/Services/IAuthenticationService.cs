using BtcDemo.Core.DTOs;
using BtcDemo.Core.Utilities.Results;

namespace BtcDemo.Core.Services;

public interface IAuthenticationService
{
	Task<IDataResult<TokenDto>> CreateTokenAsync(LoginDto loginDto);
	Task<IDataResult<TokenDto>> CreateTokenByRefreshToken(string refreshToken);

	// kullanıcı logout olduktan sonra refresh tokenı sonlandırıyorum.
	// kullanıcı refresh tokenı çalınırsa burayı çağırıp null edebiliriz.
	Task<IResult> RevokeRefreshToken(string refreshToken);

}
