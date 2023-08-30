using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Requests.Queries.GetRequestById;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.Requests
{
	[Authorize]
	public class IndexModel : PageModel
    {
		private readonly IMediator _mediator;

		public Request request {
			get; set;
		}

		public List<RequestType> RequestTypes {
			get; set;
		}

		[BindProperty(SupportsGet = true)]
		public int Id {
			get; set;
		}

		public bool Disabled {
			get; set;
		}

		public IndexModel(IMediator mediator) {
			_mediator = mediator;
		}

        public async Task OnGetAsync()
        {
			request = await _mediator.Send(new GetRequestByIdQuery(Id));
			RequestTypes = Enum.GetValues(typeof(RequestType)).Cast<RequestType>().ToList();
			Disabled = true;
        }

		[Authorize(Roles = "Admin")]
		public async Task OnPostCompleteAsync() {
			
		}

		[Authorize(Roles = "Admin")]
		public async Task OnPostDelete() {
			throw new NotImplementedException();
		}

		[Authorize(Roles = "Worker")]
		public async Task OnPostUpdate() {
			throw new NotImplementedException();
		}
    }
}
