using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
	public class User : IdentityUser<int>
	{
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
		public int Age {
			get; set;
		}

		[Required]
		[MaxLength(300)]
		public string Position {
			get; set;
		}

		public List<Request> Requests {
			get; set;
		}

		public ParkingSlot ParkingSlot {
			get; set;
		}

		[NotMapped]
		public static string cacheKey => "Users";

		[NotMapped]
		public string FullName {
			get => $"{FirstName} {LastName}";
		}
	}
}

