using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstrations;
using Application.Users.Commands.CreateUser;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.Users
{
    [Authorize(Roles = "Admin")]
	[BindProperties]
	public class CreateModel : PageModel
    {
		private readonly IMediator _mediator;

		[Required]
		[MaxLength(50)]
		public string FirstName {
			get; set;
		}

		[Required]
		[MaxLength(50)]
		public string LastName {
			get; set;
		}

		[Required]
		public int Age {
			get; set;
		}

		[Required]
		[MaxLength(300)]
		public string Position {
			get; set;
		}

		[Required]
		[MaxLength(300)]
		public string Email {
			get; set;
		}

		[Required]
		[DataType(DataType.Password)]
		[MinLength(8)]
		[MaxLength(30)]
		public string Password {
			get; set;
		}

		public CreateModel(IMediator mediator) {
			_mediator = mediator;
		}

		public async void OnGet()
        {
        }

        public async Task<IActionResult> OnPost() {
			if (!ModelState.IsValid)
				return Page();

			try {
				await _mediator.Send(new CreateUserCommand(FirstName, LastName, Age, Email, Position, Password));
				return Redirect("/Users/List");
			} catch(InvalidUserCredentialsException e) {
				foreach (var error in e.errors) {
					ModelState.AddModelError("", error.Description);
				}
				return Page();
			}	
		}
    }
}
