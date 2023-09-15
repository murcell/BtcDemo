using BtcDemo.Core.Entities;
using BtcDemo.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BtcDemo.Data.Repositories;

public class CoinRepository : RepositoryBase<Coin>, ICoinRepository
{
    public CoinRepository(DbContext context) : base(context)
    {

    }
}
