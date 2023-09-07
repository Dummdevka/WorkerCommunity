using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Application.Requests.Commands.CreateRequest;
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
	public class CreateModel : PageModel
    {
		private readonly IMediator _mediator;
		private readonly UserManager<User> _userManager;

		[TempData]
        public string SubmitMessage {
            get; set;
        }

        [Required]
        [BindProperty]
        public string Title {
            get; set;
        }

		[Required]
		[BindProperty]
		public string Description {
			get; set;
		}

		[Required]
		[BindProperty]
		public RequestType Type {
			get; set;
		}

		public List<RequestType> RequestTypes {
            get; set;
        }

        public CreateModel(IMediator mediator, UserManager<User> userManager) {
			_mediator = mediator;
			_userManager = userManager;
			RequestTypes = Enum.GetValues(typeof(RequestType)).Cast<RequestType>().ToList();
		}

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid) 
				return Page();
            int id = (await _userManager.GetUserAsync(User)).Id;
            Result<int> result = await _mediator.Send(new CreateRequestCommand(Type, Title, Description, id));
            if (result.IsError) {
				result.Error.Errors.ForEach(e => ModelState.AddModelError("", e));
				return Page();
			}
			return Redirect("/Requests/List");
			
        }
    }
}
