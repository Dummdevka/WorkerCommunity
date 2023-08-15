using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
	public class User
	{
		[Key]
		public int Id {
			get; set;
		}
	
		[Required]
		[MaxLength(50)]
		public string FirstName {
			get; set;
		}

		[Required]
		[MaxLength(50)]
		public string LastName {
			get; set;
		}

		[Required]
		[EmailAddress]
		public string Email {
			get; set;
		}

		[Required]
		public int Age {
			get; set;
		}

		[Required]
		[MaxLength(300)]
		public string Position {
			get; set;
		}

		[Required]
		[MinLength(8)]
		[MaxLength(360)]
		public string Password {
			get; set;
		}
	}
}

