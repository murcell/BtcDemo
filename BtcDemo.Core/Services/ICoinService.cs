using BtcDemo.Core.DTOs;
using BtcDemo.Core.Entities;
using BtcDemo.Core.Utilities.Results;
using System.Linq.Expressions;

namespace BtcDemo.Core.Services
{
	public interface ICoinService
	{
		Task<IDataResult<CoinDto>> GetAsync(long id);
		Task<IResult> AddAsync(AddCoinDto addCoinDto);
		//Task<IResult> UpdateAsync(UpdateCoinDto updateCoinDto);
		//Task<IResult> PayCoin(UpdateCoinDto updateCoinDto);
		//Task<IResult> DeleteAsync(long CoinId);
		Task<IDataResult<IEnumerable<CoinDto>>> GetAllAsync();
		Task<IDataResult<IEnumerable<CoinDto>>> GetManyAsync(Expression<Func<Coin, bool>> predicate);
	}
}
