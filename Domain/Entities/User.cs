using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
	public class User : IdentityUser<int>
	{
		//[Key]
		//public int Id {
		//	get; set;
		//}
	
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
		[MaxLength(340)]
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

		public List<Message> MessagesSent {
			get; set;
		}

		public List<Message> MessagesReceived {
			get; set;
		}

		public List<Request> Requests {
			get; set;
		}

		public ParkingSlot ParkingSlot {
			get; set;
		}
	}
}

