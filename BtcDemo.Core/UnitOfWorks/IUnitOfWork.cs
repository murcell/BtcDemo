using BtcDemo.Core.Repositories;

namespace BtcDemo.Core.UnitOfWorks;

public interface IUnitOfWork
{
	ICoinRepository Invoices { get; }
	IUserRefreshTokenRepository UserRefreshTokens { get; }
	Task<int> CommitAsync();

	void Commit();
}
