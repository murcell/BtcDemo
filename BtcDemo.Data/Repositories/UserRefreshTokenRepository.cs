using BtcDemo.Core.Entities;
using BtcDemo.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BtcDemo.Data.Repositories;

public class UserRefreshTokenRepository : RepositoryBase<UserRefreshToken>, IUserRefreshTokenRepository
{
	public UserRefreshTokenRepository(DbContext context) : base(context)
	{

	}
}
