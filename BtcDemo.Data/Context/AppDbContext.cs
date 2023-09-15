using BtcDemo.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BtcDemo.Data.Context;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
		Database.EnsureCreated();
	}

	public DbSet<Coin> Coins { get; set; }
	public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }


	protected override void OnModelCreating(ModelBuilder builder)
	{
		// mapping ayarlarını burada ekliyoruz
		builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

		base.OnModelCreating(builder);
	}

}
