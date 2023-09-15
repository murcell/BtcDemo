using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BtcDemo.Core.Entities;

namespace BtcDemo.Data.Configurations
{
	public class CoinConfiguration : IEntityTypeConfiguration<Coin>
	{
		public void Configure(EntityTypeBuilder<Coin> builder)
		{

			builder.ToTable("Coins").HasKey(b => b.Id);
			builder.Property(b => b.Id).HasColumnName("Id").IsRequired();
			builder.Property(b => b.Name).HasColumnName("Name").IsRequired();
			builder.Property(b => b.Symbol).HasColumnName("Name").IsRequired();
			builder.Property(b => b.Name).HasColumnName("Rank").IsRequired();
			builder.Property(x => x.PriceUsd).IsRequired().HasColumnType("decimal(18,2)");
			builder.Property(x => x.PercentChange24h).IsRequired().HasColumnType("decimal(18,2)");
			builder.Property(x => x.PercentChange1h).IsRequired().HasColumnType("decimal(18,2)");
			builder.Property(x => x.PercentChange7d).IsRequired().HasColumnType("decimal(18,2)");
			builder.Property(x => x.CSupply).IsRequired().HasColumnType("decimal(18,2)");
			builder.Property(x => x.TSupply).IsRequired().HasColumnType("decimal(18,2)");
			builder.Property(x => x.MSupply).IsRequired().HasColumnType("decimal(18,2)");
			builder.Property(b => b.CreatedDate).HasColumnName("CreatedDate").IsRequired();
			builder.Property(b => b.UpdatedDate).HasColumnName("UpdatedDate");
			builder.Property(b => b.DeletedDate).HasColumnName("DeletedDate");


		}
	}
}
