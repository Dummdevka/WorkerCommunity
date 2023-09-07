using System.ComponentModel;
using Application.Requests.Commands.DeleteRequest;
using Application.Requests.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.Requests
{
    [Authorize]
	public class ListModel : PageModel
    {
		private readonly IMediator _mediator;
		private readonly UserManager<User> _userManager;

		public List<Request> Requests {
            get; set;
        }

        [BindProperty(SupportsGet = true)]
        [DefaultValue(false)]
        public bool Finished {
            get; set;
        }

        public ListModel(IMediator mediator, UserManager<User> userManager) {
			_mediator = mediator;
			_userManager = userManager;
		}

        public async Task<IActionResult> OnGetAsync()
        {
            if (User.IsInRole("Admin")) {
                Requests = await _mediator.Send(new GetRequestsQuery(Completed: Finished));
            } else {
                int userId = (await _userManager.GetUserAsync(User)).Id;
                Requests = await _mediator.Send(new GetRequestsQuery(UserId: userId));
	        }
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id) {
            await _mediator.Send(new DeleteRequestCommand(id));
            return RedirectToAction(Request.Path);
	    }
	}
}
