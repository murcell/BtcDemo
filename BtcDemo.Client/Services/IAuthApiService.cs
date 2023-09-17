using BtcDemo.Client.Models;

namespace BtcDemo.Client.Services
{
	public interface IAuthApiService
	{
        Task<bool> Login(UserLoginModel model);
    }
}
