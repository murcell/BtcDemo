using BtcDemo.Core.Repositories;

namespace BtcDemo.Core.UnitOfWorks;

public interface IUnitOfWork
{
	ICoinRepository Coins { get; }
	IUserRefreshTokenRepository UserRefreshTokens { get; }
	Task<int> CommitAsync();

	void Commit();
}
