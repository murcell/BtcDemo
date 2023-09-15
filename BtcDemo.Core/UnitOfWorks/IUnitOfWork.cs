using BtcDemo.Core.Repositories;

namespace BtcDemo.Core.UnitOfWorks;

public interface IUnitOfWork
{
	ICryptoCurrencyRepository Invoices { get; }
	IUserRefreshTokenRepository UserRefreshTokens { get; }
	Task<int> CommitAsync();

	void Commit();
}
