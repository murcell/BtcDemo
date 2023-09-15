using BtcDemo.Core.Entities.Base;

namespace BtcDemo.Core.Entities;

public class UserRefreshToken:Entity<long>
{
	public string UserId { get; set; }
	public string Code { get; set; }
	public DateTime Expiration { get; set; }
}
