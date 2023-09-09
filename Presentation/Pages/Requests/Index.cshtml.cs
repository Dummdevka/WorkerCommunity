using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Application.Requests.Commands.DeleteRequest;
using Application.Requests.Commands.UpdateRequest;
using Application.Requests.Queries.GetRequestById;
using Domain.Entities;
using Domain.Enums;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.Requests
{
	[Authorize]
	public class IndexModel : PageModel
    {
		private readonly IMediator _mediator;
		private readonly UserManager<User> _userManager;

		public Request request {
			get; set;
		}
		//[BindProperty]
		//[Required]
		//[MaxLength(200)]
		//public string Title {
		//	get; set;
		//}

		//[BindProperty]
		//[Required]
		//[MaxLength(500)]
		//public string Description {
		//	get; set;
		//}

		//public bool Completed {
		//	get; set;
		//}
		public class RequestTypeOption
		{
			public RequestType type {
				get; set;
			}

			public bool selected {
				get; set;
			}
		}

		public List<RequestTypeOption> RequestTypes {
			get; set;
		} = new();

		[BindProperty(SupportsGet = true)]
		public int Id {
			get; set;
		}

		public bool Disabled {
			get; set;
		}

		public IndexModel(IMediator mediator, UserManager<User> userManager) {
			_mediator = mediator;
			_userManager = userManager;
		}

        public async Task<IActionResult> OnGetAsync()
        {
			Result<Request> result = await _mediator.Send(new GetRequestByIdQuery(Id));

			if (!User.IsInRole("Admin") && result.Value.UserId != (await _userManager.GetUserAsync(User)).Id) {
				return Redirect("/Requests/List");
			}

			request = result.Value;

			foreach (var type in Enum.GetValues(typeof(RequestType)).Cast<RequestType>().ToList()) {
				RequestTypeOption requestType = new() {
					type = type,
					selected = type == request.RequestType ? true : false
				};
				RequestTypes.Add(requestType);
			}
			
			Disabled = User.IsInRole("Worker") ? false : true;
			return Page();
        }


		public async Task<IActionResult> OnPostDelete() {
            Domain.Shared.EmptyResult result = await _mediator.Send(new DeleteRequestCommand(Id));
			if (result.IsError) {
				foreach (var error in result.Error.Errors) {
					ModelState.AddModelError("", error);
				}
				return Page();
			}
			return Redirect("/Requests/List");

		}

		public async Task<IActionResult> OnPostUpdateAsync(Request request) {
			request.UserId = (await _userManager.GetUserAsync(User)).Id;
			Result<Request> result = await _mediator.Send(new UpdateRequestCommand(request));
			if (result.IsError) {
				foreach (var error in result.Error.Errors) {
					ModelState.AddModelError("", error);
				}
			}

			return RedirectToAction("OnGetAsync", new { Id = result.Value.Id });
		}

		public IActionResult OnGetEdit() {
			Disabled = false;
			return Page();
		}

		protected void checkUser() {
		
		}
	}
}
