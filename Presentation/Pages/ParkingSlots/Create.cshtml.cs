using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Application.ParkingSlots.Commands.CreateParkingSlot;
using Domain.Entities;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.ParkingSlots
{
	public class CreateModel : PageModel
    {
		private readonly IMediator _mediator;

        [Required]
        [MaxLength(10)]
        [BindProperty]
		public string Title {
            get; set;
        }

        public CreateModel(IMediator mediator) {
			_mediator = mediator;
		}

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync() {
            Result<int> result = await _mediator.Send(new CreateParkingSlotCommand(Title));
            if (result.IsError) {
				foreach (var error in result.Error.Errors) {
					ModelState.AddModelError("", error);
				}
				return Page();
			}
            return Redirect("/ParkingSlots/List");
               
	    }
    }
}
