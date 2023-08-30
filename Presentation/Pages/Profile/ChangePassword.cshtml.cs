using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Users.Commands.UpdateUserPassword;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.Profile
{
    [Authorize]
	public class ChangePasswordModel : PageModel
    {
		private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;

        [BindProperty]
		public string newPassword {
            get; set;
        }

        [BindProperty]
		public string oldPassword {
			get; set;
		}

		public ChangePasswordModel(UserManager<User> userManager, IMediator mediator) {
			_userManager = userManager;
            _mediator = mediator;
		}

        [TempData]
        public string SubmitMessage {
            get; set;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync() {
            string userId = _userManager.GetUserId(User);

            try {
                await _mediator.Send(new UpdateUserPasswordCommand(userId, oldPassword, newPassword));
                SubmitMessage = "Password has been successfully changed!";
                return RedirectToAction(Request.Path);
			}
			catch (InvalidUserCredentialsException e) {
                foreach (var error in e.errors) {
                    ModelState.AddModelError("", error.Description);
		        }
	        }
            return Page();
	    }
    }
}
