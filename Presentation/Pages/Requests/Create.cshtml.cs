using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.Requests
{
    [Authorize]
	public class CreateModel : PageModel
    {
		private readonly IMediator _mediator;

		[TempData]
        public string SubmitMessage {
            get; set;
        }

        public CreateModel(IMediator mediator) {
			_mediator = mediator;
		}

        public void OnGet()
        {
        }

     //   public async Task<IActionResult> OnPostAsync() {
            
	    //}
    }
}
