using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.ParkingSlots
{
	public class CreateModel : PageModel
    {
		private readonly IMediator _mediator;

		public string Title {
            get; set;
        }

        public CreateModel(IMediator mediator) {
			_mediator = mediator;
		}

        public void OnGet()
        {
        }

        public async Task OnPostAsync() {
        
	    }
    }
}
