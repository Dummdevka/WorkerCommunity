using System;
namespace Application.Options
{
	public class CookieOptions
	{
		public object Cookie {
			get; init;
		}

		public string LoginPath {
			get; init;
		}

		public string AccessDeniedPath {
			get; init;
		}

		public bool SlidingExpiration {
			get; init;
		}

		public TimeSpan ExpireTimeSpan {
			get; set;
		}
	}
}

