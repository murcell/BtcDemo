using BtcDemo.Core.DTOs;
using BtcDemo.Core.Utilities.Results;

namespace BtcDemo.Core.Services;

public interface IUserService
{
	Task<IDataResult<UserDto>> CreateUserAsync(CreateUserDto createUserDto);
	Task<IDataResult<UserDto>> GetUserByEmail(string email);
	Task<IDataResult<UserDto>> GetUserByName(string userName);
}