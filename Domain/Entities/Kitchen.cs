using System;
using System.ComponentModel;

namespace Domain.Entities
{
	public class Kitchen
	{
		[DefaultValue(false)]
		public bool waterNeeded {
			get; set;
		}

		[DefaultValue(false)]
		public bool teaNeeded {
			get; set;
		}

		[DefaultValue(false)]
		public bool coffeeNeeded {
			get; set;
		}
	}
}

