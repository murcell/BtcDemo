using BtcDemo.Core.DTOs;
using BtcDemo.Core.Utilities.Results;

namespace BtcDemo.Core.Services;

public interface IUserService
{
	Task<IDataResult<AppUserDto>> CreateUserAsync(CreateAppUserDto createUserDto);
	Task<IDataResult<AppUserDto>> GetUserByEmail(string email);
	Task<IDataResult<AppUserDto>> GetUserByName(string userName);
	Task<IDataResult<IList<AppUserDto>>> GetAllUsers();
}