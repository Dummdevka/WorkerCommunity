using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
	public class Message
	{
		[Key]
		public int Id {
			get; set;
		}

		[Required]
		[MaxLength(2000)]
		public string Text {
			get; set;
		}

		[Required]
		public User From {
			get; set;
		}

		[Required]
		public User To {
			get; set;
		}
	}
}

