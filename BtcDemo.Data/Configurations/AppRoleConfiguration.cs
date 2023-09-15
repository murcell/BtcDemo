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
	public class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
	{
		public void Configure(EntityTypeBuilder<AppRole> builder)
		{
			builder.HasData(
				new AppRole
				{
					Id = Guid.NewGuid().ToString(),
					Name = "Viewer",
					NormalizedName = "VIEWER",
					ConcurrencyStamp = Guid.NewGuid().ToString()
				}
				

			);
		}
	}
}
