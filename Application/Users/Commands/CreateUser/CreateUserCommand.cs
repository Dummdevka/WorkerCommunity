using MediatR;

namespace Application.Users.Commands.CreateUser
{
	public record CreateUserCommand(string firstname, string lastname, int age, string email, string position, string password) : IRequest<int>;
}

