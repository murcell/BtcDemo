using AutoMapper;
using BtcDemo.Core.ComplexTypes;
using BtcDemo.Core.UnitOfWorks;
using BtcDemo.Core.Utilities.Results;
using BtcDemo.Core.Utilities.ValidaitonError;
using BtcDemo.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BtcDemo.Core.Services;
using BtcDemo.Core.DTOs;
using BtcDemo.Core.Entities;

namespace BtcDemo.Service.Services;

public class CoinService : ServiceBase, ICoinService
{
	public CoinService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
	{

	}

	public async Task<IResult> AddAsync(AddCoinDto addCoinDto)
	{
		var Coin = Mapper.Map<Coin>(addCoinDto);
		
		// Burada eklenecek bir şey olursa ekleyeceğiz.

		await UnitOfWork.Coins.AddAsync(Coin);
		await UnitOfWork.CommitAsync();
		return new Result(ResultStatus.Success, Messages.CoinMessage.Add());
	}

	public async Task<IResult> DeleteAsync(long id)
	{
		var result = await UnitOfWork.Coins.AnyAsync(a => a.Id == id);
		if (result)
		{
			List<Expression<Func<Coin, bool>>> predicates = new List<Expression<Func<Coin, bool>>>();

			predicates.Add(a => a.Id == id);

			var Coin = await UnitOfWork.Coins.GetAsync(predicates);

			Coin.IsDeleted = true;
			await UnitOfWork.Coins.UpdateAsync(Coin);
			await UnitOfWork.CommitAsync();
			return new Result(ResultStatus.Success, Messages.CoinMessage.Delete(Coin.Id.ToString()));
		}
		return new Result(ResultStatus.Error, Messages.CoinMessage.NotFound(isPlural: false));
	}

	public async Task<IDataResult<IEnumerable<CoinDto>>> GetAllAsync()
	{
		var Coins = await UnitOfWork.Coins.GetAllAsync();
		if (Coins.ToList().Count > 0)
		{
			var ssb = Mapper.Map<List<CoinDto>>(Coins);
			return new DataResult<IList<CoinDto>>(ResultStatus.Success, ssb);
		}
		return new DataResult<IList<CoinDto>>(ResultStatus.Error, new List<CoinDto>());
	}

	public async Task<IDataResult<CoinDto>> GetAsync(long id)
	{
		List<Expression<Func<Coin, bool>>> predicates = new List<Expression<Func<Coin, bool>>>();

		predicates.Add(a => a.Id == id);

		var Coin = await UnitOfWork.Coins.GetAsync(predicates);

		if (Coin == null)
		{
			return new DataResult<CoinDto>(ResultStatus.Warning, Messages.General.ValidationError(), null, new List<ValidationError> { new ValidationError { PropertyName = "CoinNo", Message = Messages.CoinMessage.NotFound(false) } });
		}

		return new DataResult<CoinDto>(ResultStatus.Success, Mapper.Map<CoinDto>(Coin));
	}

	public async Task<IDataResult<IEnumerable<CoinDto>>> GetManyAsync(Expression<Func<Coin, bool>> predicate)
	{
		var Coins = await UnitOfWork.Coins.GetManyAsync(predicate);

		if (Coins == null)
		{
			return new DataResult<IList<CoinDto>>(ResultStatus.Warning, "Bulunamadı", null);
		}

		return new DataResult<IList<CoinDto>>(ResultStatus.Success, Mapper.Map<IList<CoinDto>>(Coins));
	}


}
