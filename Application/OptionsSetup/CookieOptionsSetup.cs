using System;
using Application.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Application.OptionsSetup
{
	public class CookieOptionsSetup : IConfigureOptions<CookieOptions>
	{
		private readonly IConfiguration _config;

		public CookieOptionsSetup(IConfiguration config)
		{
			_config = config;
		}

		public void Configure(CookieOptions options)
		{
			_config.GetSection(nameof(CookieOptions)).Bind(options);
			options.ExpireTimeSpan = TimeSpan.FromHours(2);
		}
	}
}

