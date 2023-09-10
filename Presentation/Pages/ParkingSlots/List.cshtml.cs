using Application.ParkingSlots.Commands.DeleteParkingSlot;
using Application.ParkingSlots.Commands.OccupyParkingSlot;
using Application.ParkingSlots.Queries.GetParkingSlots;
using Domain.Entities;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.ParkingSlots
{
	public class ListModel : PageModel
    {
		private readonly IMediator _mediator;
		private readonly UserManager<User> _userManager;

		public List<ParkingSlot> ParkingSlots {
            get; set;
        }

        public ListModel(IMediator mediator, UserManager<User> userManager) {
			_mediator = mediator;
			_userManager = userManager;
		}

        public async Task OnGetAsync()
        {
			Result<List<ParkingSlot>> result = await _mediator.Send(new GetParkingSlotsQuery());
			ParkingSlots = result.Value;
        }

        public async Task<IActionResult> OnPostOccupyAsync(int id) {
			int userId = (await _userManager.GetUserAsync(User)).Id;
			Result<ParkingSlot> result = await _mediator.Send(new OccupyParkingSlotCommand(id, userId));
			return RedirectToAction("OnGetAsync");
	    }

		public async Task<IActionResult> OnPostDeleteAsync(int id) {
			await _mediator.Send(new DeleteParkingSlotCommand(id));
			return RedirectToAction("OnGetAsync");
		}

		public async Task<IActionResult> OnGetMyslotAsync() {
			int userId = (await _userManager.GetUserAsync(User)).Id;
			Result<List<ParkingSlot>> result = await _mediator.Send(new GetParkingSlotsQuery() { userId = userId });
			ParkingSlots = result.Value;
			return Page();
		}

		public async Task<IActionResult> OnGetFreeslotsAsync() {
			int userId = (await _userManager.GetUserAsync(User)).Id;
			Result<List<ParkingSlot>> result = await _mediator.Send(new GetParkingSlotsQuery() { Occupied = false });
			ParkingSlots = result.Value;
			return Page();
		}
	}
}
