using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
	public class ParkingSlot
	{
		[Key]
		public int Id {
			get; set;
		}

		[Required]
		[MaxLength(10)]
		public string Name {
			get; set;
		}

		public int UserId {
			get; set;
		}

		public User OccupiedBy {
			get; set;
		}
	}
}

