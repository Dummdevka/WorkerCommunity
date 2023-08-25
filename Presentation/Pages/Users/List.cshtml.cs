using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Users.Commands.DeleteUser;
using Application.Users.Queries.GetUsers;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.Users
{
	public class ListModel : PageModel
    {
        public IMediator _mediator {
            get; set;
        }

        public List<User> Users {
            get; set;
        }

        public ListModel(IMediator mediator) {
            _mediator = mediator;
        }

        [Authorize]
        public async Task OnGet()
        {
            Users = await _mediator.Send(new GetUsersQuery());
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id) {
            await _mediator.Send(new DeleteUserCommand(id));
            return Redirect("/Users/List");
	    }
    }
}
