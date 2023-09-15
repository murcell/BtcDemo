using Microsoft.AspNetCore.Identity;
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
	public class UserAppConfiguration : IEntityTypeConfiguration<AppUser>
	{
		public void Configure(EntityTypeBuilder<AppUser> builder)
		{
			
			var defaultAppUser = new AppUser
			{
				Id = Guid.NewGuid().ToString(),
				UserName = "coin@appcoin.com",
				NormalizedUserName = "TELLER@ODEMEMERKEZI.COM",
				Email = "coin@appcoin.com",
				NormalizedEmail = "COIN@APPCOIN.COM",
				PhoneNumber = "+909999999999",
				EmailConfirmed = true,
				PhoneNumberConfirmed = true,
				SecurityStamp = Guid.NewGuid().ToString()
			};
			// bura
			defaultAppUser.PasswordHash = CreatePasswordHash(defaultAppUser, ".7mp?*reP!");

			builder.HasData(defaultAppUser);
		}

		private string CreatePasswordHash(AppUser user, string password)
		{
			var passwordHasher = new PasswordHasher<AppUser>();
			return passwordHasher.HashPassword(user, password);
		}
	}
}
