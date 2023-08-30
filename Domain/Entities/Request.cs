using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities
{
	public class Request
	{
		[Key]
		public int Id {
			get; set;
		}

		[Required]
		public RequestType RequestType {
			get; set;
		}

		[Required]
		[MaxLength(200)]
		public string Title {
			get; set;
		}

		[MaxLength(500)]
		public string Description {
			get; set;
		}

		//[Required]
		public int UserId {
			get; set;
		}
		//[Required]
		public User CreatedBy {
			get; set;
		}

		public bool Completed {
			get; set;
		} = false;

		[NotMapped]
		public static string cacheKey = "Requests";
	}

	
}

