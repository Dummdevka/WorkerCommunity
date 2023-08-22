using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Application.OptionsSetup
{
	public class IdentityOptionsSetup : IConfigureOptions<IdentityOptions>
	{
		private readonly IConfiguration _config;

		public IdentityOptionsSetup(IConfiguration config)
		{
			_config = config;
		}

		public void Configure(IdentityOptions options)
		{
			_config.GetSection(nameof(IdentityOptions)).Bind(options);
		}
	}
}

