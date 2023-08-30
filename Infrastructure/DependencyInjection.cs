using Application.Abstractions;
using Infrastructure.Persistance;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Application.Abstrations;
using Infrastructure.Caching;
using Microsoft.AspNetCore.Identity;
using Domain.Entities;
using Application.OptionsSetup;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection addInfrastructure(this IServiceCollection services, IConfiguration config, IHostBuilder host) {
		//Logging 
		var logger = new LoggerConfiguration()
			.ReadFrom.Configuration(config)
			.Enrich.FromLogContext()
			.WriteTo.File(path: Environment.CurrentDirectory + "/logs/log-.txt", rollingInterval: RollingInterval.Day)
			.CreateLogger();

		host.UseSerilog(logger);

		//Authentication
		services
			   .AddDbContext<CommunityDbContext>(options =>
			   {
				   options.UseSqlServer(config.GetConnectionString("Default"));
				   options.EnableSensitiveDataLogging();
			   })

				.AddDefaultIdentity<User>()
			   .AddRoles<IdentityRole<int>>()
			   .AddEntityFrameworkStores<CommunityDbContext>();
		

		services.ConfigureOptions<IdentityOptionsSetup>();
		services.ConfigureOptions<CookieOptionsSetup>();

		return	services
				.AddScoped<IDbContext, CommunityDbContext>()
				.AddSingleton<ICachingService, CachingService>();
	}

	public static async Task AddRoles(this WebApplication app) {

		using (var scope = app.Services.CreateScope()) {
			var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

			var roles = new[] { "Admin", "Worker" };

			foreach (var role in roles) {
				if (!await roleManager.RoleExistsAsync(role)) {
					await roleManager.CreateAsync(new IdentityRole<int>(role));
				}
			}
		}
    }

	public static async Task AddAdmin(this WebApplication app) {
		string email = "trake1524@gmail.com";
		string password = "Secret123!";

		using (var scope = app.Services.CreateScope()) {
			var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
			var logger = scope.ServiceProvider.GetRequiredService<ILogger<User>>();
			if (await userManager.FindByEmailAsync(email) is null) {
				var user = new User();
				user.UserName = "Test";
				user.FirstName = "Admin";
				user.LastName = "Admin";
				user.Age = 22;
				user.Position = "Owner";
				user.Email = email;
				user.SecurityStamp = Guid.NewGuid().ToString("D");
	
				var result = await userManager.CreateAsync(user, password);

				if (!result.Succeeded) {
					logger.LogDebug("Error creating test user: " + result.Errors.ToString());
				}

				await userManager.AddToRoleAsync(user, "Admin");
			}
		}
    }
}

