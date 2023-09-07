using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Users.Commands.DeleteUser;
using Application.Users.Queries.GetUsers;
using Domain.Entities;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EmptyResult = Domain.Shared.EmptyResult;

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
            Result<List<User>> result = await _mediator.Send(new GetUsersQuery());
            Users = result.Value;
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id) {
            EmptyResult result =  await _mediator.Send(new DeleteUserCommand(id));
            if (result.IsError) {
                result.Error.Errors.ForEach(e => ModelState.AddModelError("", e));
	        }
            return Redirect("/Users/List");
	    }
    }
}
