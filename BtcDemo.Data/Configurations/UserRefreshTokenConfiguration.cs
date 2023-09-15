using BtcDemo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BtcDemo.Data.Configurations;

public class UserRefreshTokenConfiguration : IEntityTypeConfiguration<UserRefreshToken>
{
	public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
	{
		builder.HasKey(x => x.Id);
		builder.Property(b => b.UserId).HasColumnName("UserId").IsRequired();
		builder.Property(x => x.Code).HasColumnName("Code").IsRequired().HasMaxLength(500);
		builder.Property(b => b.CreatedDate).HasColumnName("CreatedDate").IsRequired();
		builder.Property(b => b.UpdatedDate).HasColumnName("UpdatedDate");
		builder.Property(b => b.DeletedDate).HasColumnName("DeletedDate");
	}
}
