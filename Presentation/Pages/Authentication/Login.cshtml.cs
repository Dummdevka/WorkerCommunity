using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace Presentation.Pages.Authentication
{
	//[BindProperties]
    public class LoginModel : PageModel
    {
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;

		[BindProperty]
		public string Email {
			get; set;
		}

		[BindProperty]
		public string Password {
			get; set;
		}

		public string ReturnUrl {
			get; set;
		}

		//public LoginModel() {

		//}
		public LoginModel(UserManager<User> userManager, SignInManager<User> signInManager) {
			_userManager = userManager;
			_signInManager = signInManager;
		}
		public void OnGet()
        {
        }

		public async Task<IActionResult?> OnPost() {

			User? user = await _userManager.FindByEmailAsync(Email);
			if (user is not null) {
				var result = await _signInManager.PasswordSignInAsync(user, Password, false, false);

				if (result.Succeeded) {
					if (!string.IsNullOrEmpty(ReturnUrl)) {
						return Redirect(ReturnUrl);
					} else {
						return Redirect("/");
					}
				} else {
					ModelState.AddModelError("", "Invalid password");
				}
			}
			ModelState.AddModelError("", "Invalid email");

			return Page();
		}
	}
}
