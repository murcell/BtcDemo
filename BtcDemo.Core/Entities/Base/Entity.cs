using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BtcDemo.Core.Entities.Base
{
	public class Entity<TId> : IEntityTimestamps
	{
		public TId Id { get; set; }

		public DateTime CreatedDate { get; set; }

		public DateTime? UpdatedDate { get; set; }

		public DateTime? DeletedDate { get; set; }

		// farklı kullanımlar için constructor
		// bunları yapmak zorunda değiliz
		public Entity()
		{
			Id = default;

		}

		public Entity(TId id)
		{
			Id = id;
		}
	}
}
