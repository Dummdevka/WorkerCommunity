using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.ParkingSlots
{
	public class ListModel : PageModel
    {
		private readonly IMediator _mediator;

		public List<ParkingSlot> ParkingSlots {
            get; set;
        }

        public ListModel(IMediator mediator) {
			_mediator = mediator;
		}

        public void OnGet()
        {
        }

        public async Task OnPostOccupy(int id) {
            
	    }

		public async Task OnPostDelete(int id) {

		}
	}
}
