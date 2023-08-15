using Application.Abstractions;
using Infrastructure.Persistance;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection addApplication(this IServiceCollection services) {
		return services.AddScoped<IDbContext, CommunityDbContext>();
	}
}

