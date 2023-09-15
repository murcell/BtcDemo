using BtcDemo.Core.DTOs;
using BtcDemo.Core.Entities;
using BtcDemo.Core.Utilities.Results;
using System.Linq.Expressions;

namespace BtcDemo.Core.Services
{
	public interface ICryptoCurrencyService
	{
		Task<IDataResult<CryptoCurrencyDto>> GetAsync(long id);
		Task<IResult> AddAsync(AddCryptoCurrencyDto addCryptoCurrencyDto);
		//Task<IResult> UpdateAsync(UpdateCryptoCurrencyDto updateCryptoCurrencyDto);
		//Task<IResult> PayCryptoCurrency(UpdateCryptoCurrencyDto updateCryptoCurrencyDto);
		//Task<IResult> DeleteAsync(long CryptoCurrencyId);
		Task<IDataResult<IEnumerable<CryptoCurrencyDto>>> GetAllAsync();
		Task<IDataResult<IEnumerable<CryptoCurrencyDto>>> GetManyAsync(Expression<Func<CryptoCurrency, bool>> predicate);
	}
}
