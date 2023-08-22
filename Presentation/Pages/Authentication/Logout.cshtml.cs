using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.Authentication
{
	public class LogoutModel : PageModel
    {
		private readonly SignInManager<User> _signInManager;

		public LogoutModel(SignInManager<User> signInManager) {
			_signInManager = signInManager;
		}
		public async Task<IActionResult> OnPost()
        {
			await _signInManager.SignOutAsync();
			return Redirect("/");
		}
    }
}
