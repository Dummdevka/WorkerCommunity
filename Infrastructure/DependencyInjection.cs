using Application.Abstractions;
using Infrastructure.Persistance;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Application.Abstrations;
using Infrastructure.Caching;

namespace Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection addInfrastructure(this IServiceCollection services, IConfiguration config, IHostBuilder host) {
		//Logging 
		var logger = new LoggerConfiguration()
			.ReadFrom.Configuration(config)
			//.MinimumLevel.Warning()
			.Enrich.FromLogContext()
			.WriteTo.File(path: Environment.CurrentDirectory + "/logs/log-.txt", rollingInterval: RollingInterval.Day)
			.CreateLogger();

		host.UseSerilog(logger);

		return services
				.AddDbContext<CommunityDbContext>(options => {
					options.UseSqlServer(config.GetConnectionString("Default"));
				})
				.AddTransient<IDbContext, CommunityDbContext>()
				.AddSingleton<ICachingService, CachingService>();
	}
}

