using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

		public int FromUserId {
			get; set;
		}

		//[Required]
		//[ForeignKey("FromUserId")]
		public User From {
			get; set;
		}

		public int ToUserId {
			get; set;
		}

		//[Required]
		//[ForeignKey("ToUserId")]
		public User To {
			get; set;
		}
	}
}

