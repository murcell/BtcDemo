using BtcDemo.Core.Repositories;
using BtcDemo.Core.UnitOfWorks;
using BtcDemo.Data.Context;
using BtcDemo.Data.Repositories;

namespace BtcDemo.Data.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
	private readonly AppDbContext _context;
	
	private CoinRepository _coin;
	private UserRefreshTokenRepository _userRefreshTokenRepository;

	public UnitOfWork(AppDbContext context)
	{
		_context = context;
	}

	
	public ICoinRepository Coins => _coin ?? new CoinRepository(_context);

	public IUserRefreshTokenRepository UserRefreshTokens => _userRefreshTokenRepository ?? new UserRefreshTokenRepository(_context);

	public async ValueTask DisposeAsync()
	{
		await _context.DisposeAsync();
	}

	public async Task<int> CommitAsync()
	{
		return await _context.SaveChangesAsync();
	}

	public void Commit()
	{
		_context.SaveChanges();
	}
}
