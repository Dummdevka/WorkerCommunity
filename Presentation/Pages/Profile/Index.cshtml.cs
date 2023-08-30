using System.ComponentModel.DataAnnotations;
using Application.Users.Commands.UpdateUser;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
//using System.Web.HttpContext.Current;

namespace Presentation.Pages.Profile
{
    [Authorize]
	//[BindProperties]
	public class IndexModel : PageModel
    {
		private readonly UserManager<User> _userManager;
		private readonly IMediator _mediator;

		[TempData]
		public string SubmitMessage {
			get; set;
		}

		[Required]
		[MaxLength(50)]
		[BindProperty]
		public string FirstName {
			get; set;
		}

		[Required]
		[MaxLength(50)]
		[BindProperty]
		public string LastName {
			get; set;
		}

		[Required]
		[EmailAddress]
		[BindProperty]
		public string Email {
			get; set;
		}

		[Required]
		[BindProperty]
		public int Age {
			get; set;
		}
		//[BindProperty]
		//public User? user {
		//	get; set;
		//}

		public IndexModel(UserManager<User> userManager, IMediator mediator) {
			_userManager = userManager;
			_mediator = mediator;
		}

        public async Task OnGetAsync()
        {
			await SetUser();
				//return Redirect("/Index");
			//if
			//user.ToString();
			
		}

		public async Task<IActionResult> OnPostUpdateAsync() {
			if (ModelState.IsValid) {
				try {
					User? user = await _userManager.GetUserAsync(User);
					user.Email = Email;
					user.FirstName = FirstName;
					user.LastName = LastName;
					user.Age = Age;
					await _mediator.Send(new UpdateUserCommand(user));
					SubmitMessage = "Data updated successfully!";
					return RedirectToAction(Request.Path);
				} catch (InvalidUserCredentialsException e) {
					foreach (var error in e.errors) {
						ModelState.AddModelError("", error.Description);
					}
				} catch (FluentValidation.ValidationException e) {
					ModelState.AddModelError("", e.Message);
				}
			}
			return Page();
			
		}

		protected async Task SetUser() {
			User? user = await _userManager.GetUserAsync(User);
			if (user is not null) {
				FirstName = user.FirstName;
				LastName = user.LastName;
				Age = user.Age;
				Email = user.Email;
			}
		}


    }
}
