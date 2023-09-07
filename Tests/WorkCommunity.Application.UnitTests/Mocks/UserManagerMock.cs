using System;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace Tests.WorkCommunity.Application.UnitTests.Mocks
{
	public class UserManagerMock
	{
		public Mock<UserManager<User>> Mock {
			get; set;
		}

		public UserManagerMock() {
			var store = new Mock<IUserStore<User>>();
			var mgr = new Mock<UserManager<User>>(store.Object,
				new Mock<IOptions<IdentityOptions>>().Object,
				new Mock<IPasswordHasher<User>>().Object,
				new IUserValidator<User>[0],
				new IPasswordValidator<User>[0],
				new Mock<ILookupNormalizer>().Object,
				new Mock<IdentityErrorDescriber>().Object,
				new Mock<IServiceProvider>().Object,
				new Mock<ILogger<UserManager<User>>>().Object);
			Mock = mgr;
		}

		public void MockCreateAsync(string? errorMessage = null) {
			Mock.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).Returns(() =>
			{
				if (errorMessage is null) {
					return Task.FromResult(IdentityResult.Success);
				} else {
					var error = new IdentityError();
					error.Description = errorMessage;
					return Task.FromResult(IdentityResult.Failed(error));
				}
			});
		}

		//public void MockFind
	}
}
