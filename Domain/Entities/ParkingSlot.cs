using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

		[ForeignKey("OccupiedBy")]
		public int? UserId {
			get; set;
		}

		public virtual User OccupiedBy {
			get; set;
		}

		[NotMapped]
		public static string cacheKey => "ParkingSlots";
	}
}

